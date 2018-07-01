using System;
using System.Text;

namespace TeslaCanBusInspector.Models
{
    public class VinMessage : IVinMessage
    {
        public const ushort TypeId = 0x508;
        public ushort MessageTypeId => TypeId;

        public byte VinPartIndex { get; }

        public string VinPartValue { get; }

        internal VinMessage()
        {
        }

        public VinMessage(byte[] payload)
        {
            if (payload.Length < 8)
            {
                throw new ArgumentException($"{nameof(payload)} must have at least 8 bytes", nameof(payload));
            }

            VinPartIndex = payload[0];
            VinPartValue = Encoding.Default.GetString(new ArraySegment<byte>(payload, 1, payload.Length - 1)).TrimEnd('\0', ' ');
        }
    }

    public interface IVinMessage : ICanBusMessage
    {
        byte VinPartIndex { get; }

        string VinPartValue { get; }
    }
}