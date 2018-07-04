using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TeslaCanBusInspector.Common.Sockets
{
    public class CanFrameQueue : ICanFrameQueue
    {
        private readonly Queue<byte[]> _queue = new Queue<byte[]>();
        private BitArray _currentBitArray;
        private int _currentPosition;

        public void Enqueue(byte[] rawBytes)
        {
            if (rawBytes?.Any() != true) return;

            _queue.Enqueue(rawBytes);
        }

        public CanFrame Dequeue(CancellationToken cancellationToken = default)
        {
            var canFrame = new CanFrame
            {
                StartOfFrame = TakeBit(cancellationToken)
            };
            var messageIdentifer = TakeNumericBits(11, cancellationToken);
            var remoteTransmission = TakeBit(cancellationToken);
            var identifierExtension = TakeBit(cancellationToken);
            var reserved = TakeBit(cancellationToken);
            var dataLengthCode = TakeNumericBits(4, cancellationToken);

            var dataLength = dataLengthCode[0] << 3 | dataLengthCode[1] << 2 | dataLengthCode[2] << 1 | dataLengthCode[3];
            var data = TakeBits(dataLength, cancellationToken);
            var crc15 = TakeNumericBits(15, cancellationToken);
            var crcDelimiter = TakeBit(cancellationToken);
            var ackSlot = TakeBit(cancellationToken);
            var ackDelimiter = TakeBit(cancellationToken);
            var endOfFrame = TakeNumericBits(7, cancellationToken);

            // TODO: put into CanFrame

            return canFrame;
        }

        private bool TakeBit(CancellationToken cancellationToken)
        {
            var bitArray = EnsureHasData(cancellationToken);
            return bitArray[_currentPosition++];
        }

        private bool[] TakeBits(int numberOfBits, CancellationToken cancellationToken)
        {
            var result = new bool[numberOfBits];
            for (var i = 0; i < numberOfBits; i++)
            {
                result[i] = TakeBit(cancellationToken);
            }

            return result;
        }

        private byte[] TakeNumericBits(int numberOfBits, CancellationToken cancellationToken)
        {
            var result = new byte[numberOfBits];
            for (var i = 0; i < numberOfBits; i++)
            {
                result[i] = TakeBit(cancellationToken) ? (byte)1 : (byte)0;
            }

            return result;
        }

        private BitArray EnsureHasData(CancellationToken cancellationToken)
        {
            var bitArray = _currentBitArray;
            if (_currentPosition >= bitArray.Count)
            {
                bitArray = null;
                _currentPosition = 0;
            }

            if (bitArray == null)
            {
                byte[] buffer;
                while (!_queue.TryDequeue(out buffer))
                {
                    if (cancellationToken.WaitHandle.WaitOne(1))
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }

                bitArray = new BitArray(buffer);
                _currentBitArray = bitArray;
            }

            return bitArray;
        }
    }

    public interface ICanFrameQueue
    {
        void Enqueue(byte[] rawBytes);

        CanFrame Dequeue(CancellationToken cancellationToken = default);
    }
}
