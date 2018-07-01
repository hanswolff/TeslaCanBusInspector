using System;

namespace TeslaCanBusInspector.Models
{
    public class BrakePedalPositionMessage : IBrakePedalPositionMessage
    {
        public const ushort TypeId = 0x168;
        public ushort MessageTypeId => TypeId;

        public byte BrakePedalPositionPercent { get; }

        internal BrakePedalPositionMessage()
        {
        }

        public BrakePedalPositionMessage(byte[] payload)
        {
            if (payload.Length < 2)
            {
                throw new ArgumentException($"{nameof(payload)} must have at least 2 bytes", nameof(payload));
            }

            BrakePedalPositionPercent = (byte)(payload[0] + (payload[1] << 8) - 3239);
        }
    }

    public interface IBrakePedalPositionMessage : ICanBusMessage
    {
        byte BrakePedalPositionPercent { get; }
    }
}