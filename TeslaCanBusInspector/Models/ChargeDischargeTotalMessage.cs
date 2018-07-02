// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.ValueTypes;

namespace TeslaCanBusInspector.Models
{
    public class ChargeDischargeTotalMessage : IChargeDischargeTotalMessage
    {
        public const ushort TypeId = 0x3D2;
        public ushort MessageTypeId => TypeId;

        public KiloWattHours ChargeTotal { get; }
        public KiloWattHours DischargeTotal { get; }

        internal ChargeDischargeTotalMessage()
        {
        }

        public ChargeDischargeTotalMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            ChargeTotal = new KiloWattHours((payload[0] + (payload[1] << 8) + (payload[2] << 16) + (payload[3] << 24)) / 1000.0m);
            DischargeTotal = new KiloWattHours((payload[4] + (payload[5] << 8) + (payload[6] << 16) + (payload[7] << 24)) / 1000.0m);
        }
    }

    public interface IChargeDischargeTotalMessage : ICanBusMessage
    {
        KiloWattHours ChargeTotal { get; }
        KiloWattHours DischargeTotal { get; }
    }
}