using System;

namespace TeslaCanBusInspector.Models
{
    public class ChargeTotalMessage : IChargeTotalMessage
    {
        public const ushort TypeId = 0x3D2;
        public ushort MessageTypeId => TypeId;

        public decimal ChargeTotalKwh { get; }

        internal ChargeTotalMessage()
        {
        }

        public ChargeTotalMessage(byte[] payload)
        {
            if (payload.Length < 4)
            {
                throw new ArgumentException($"{nameof(payload)} must have at least 4 bytes", nameof(payload));
            }

            ChargeTotalKwh = (payload[0] + (payload[1] << 8) + (payload[2] << 16) + (payload[3] << 24)) / 1000.0m;
        }
    }

    public interface IChargeTotalMessage : ICanBusMessage
    {
        decimal ChargeTotalKwh { get; }
    }
}