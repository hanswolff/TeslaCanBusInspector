// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Models
{
    public class ChargeDischargeTotalMessage : IChargeDischargeTotalMessage
    {
        public const ushort TypeId = 0x3D2;
        public ushort MessageTypeId => TypeId;

        public decimal ChargeTotalKwh { get; }
        public decimal DischargeTotalKwh { get; }

        internal ChargeDischargeTotalMessage()
        {
        }

        public ChargeDischargeTotalMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            ChargeTotalKwh = (payload[0] + (payload[1] << 8) + (payload[2] << 16) + (payload[3] << 24)) / 1000.0m;
            DischargeTotalKwh = (payload[4] + (payload[5] << 8) + (payload[6] << 16) + (payload[7] << 24)) / 1000.0m;
        }
    }

    public interface IChargeDischargeTotalMessage : ICanBusMessage
    {
        decimal ChargeTotalKwh { get; }
        decimal DischargeTotalKwh { get; }
    }
}