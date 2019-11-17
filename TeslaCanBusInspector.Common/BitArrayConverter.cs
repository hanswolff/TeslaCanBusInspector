using System;
using System.Collections;

namespace TeslaCanBusInspector.Common
{
    public static class BitArrayConverter
    {
        public static short ToInt16(byte[] bytes, int bitOffset, byte bitLength)
        {
            const int maxBits = sizeof(short) * 8;
            Guard(bytes, bitOffset, bitLength, maxBits);

            var source = new BitArray(bytes);
            var dest = new BitArray(maxBits);
            var destOffset = maxBits - bitLength;

            for (var i = 0; i < bitLength; i++)
            {
                if (source.Get(bitOffset + i))
                {
                    dest.Set(destOffset + i, true);
                }
            }

            var buf = new byte[maxBits / 8];
            dest.CopyTo(buf, 0);
            return BitConverter.ToInt16(buf);
        }

        public static int ToInt32(byte[] bytes, int bitOffset, byte bitLength)
        {
            const int maxBits = sizeof(int) * 8;
            Guard(bytes, bitOffset, bitLength, maxBits);

            var source = new BitArray(bytes);
            var dest = new BitArray(maxBits);
            var destOffset = maxBits - bitLength;

            for (var i = 0; i < bitLength; i++)
            {
                if (source.Get(bitOffset + i))
                {
                    dest.Set(destOffset + i, true);
                }
            }

            var buf = new byte[maxBits / 8];
            dest.CopyTo(buf, 0);
            return BitConverter.ToInt32(buf);
        }

        public static ushort ToUInt16(byte[] bytes, int bitOffset, byte bitLength)
        {
            const int maxBits = sizeof(ushort) * 8;
            Guard(bytes, bitOffset, bitLength, maxBits);

            var source = new BitArray(bytes);
            var dest = new BitArray(maxBits);

            for (var i = 0; i < bitLength; i++)
            {
                if (source.Get(bitOffset + i))
                {
                    dest.Set(i, true);
                }
            }

            var buf = new byte[maxBits / 8];
            dest.CopyTo(buf, 0);
            return BitConverter.ToUInt16(buf);
        }

        public static uint ToUInt32(byte[] bytes, int bitOffset, byte bitLength)
        {
            const int maxBits = sizeof(uint) * 8;
            Guard(bytes, bitOffset, bitLength, maxBits);

            var source = new BitArray(bytes);
            var dest = new BitArray(maxBits);
            var destOffset = maxBits - bitLength;

            for (var i = 0; i < bitLength; i++)
            {
                if (source.Get(bitOffset + i))
                {
                    dest.Set(destOffset + i, true);
                }
            }

            var buf = new byte[maxBits / 8];
            dest.CopyTo(buf, 0);
            return BitConverter.ToUInt32(buf);
        }

        private static void Guard(byte[] bytes, int bitOffset, byte bitLength, byte maxLength)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            if (bitOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bitOffset),
                    $"{nameof(bitOffset)} must be larger than 0 (actual value: {bitOffset})");
            }

            if (bitLength > maxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(bitLength),
                    $"{nameof(bitLength)} cannot be larger than {maxLength} (actual value: {bitLength})");
            }
        }
    }
}
