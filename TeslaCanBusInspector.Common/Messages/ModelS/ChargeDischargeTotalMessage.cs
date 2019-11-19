using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class ChargeDischargeTotalMessage : IChargeDischargeTotalMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x3D2;
        public byte RequireBytes => 8;

        public KiloWattHour ChargeTotal { get; }
        public KiloWattHour DischargeTotal { get; }

        internal ChargeDischargeTotalMessage()
        {
        }

        public ChargeDischargeTotalMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            ChargeTotal = new KiloWattHour((payload[0] + (payload[1] << 8) + (payload[2] << 16) + (payload[3] << 24)) / 1000.0m);
            DischargeTotal = new KiloWattHour((payload[4] + (payload[5] << 8) + (payload[6] << 16) + (payload[7] << 24)) / 1000.0m);
        }
    }

    public interface IChargeDischargeTotalMessage : ICanBusMessage
    {
        KiloWattHour ChargeTotal { get; }
        KiloWattHour DischargeTotal { get; }
    }
}