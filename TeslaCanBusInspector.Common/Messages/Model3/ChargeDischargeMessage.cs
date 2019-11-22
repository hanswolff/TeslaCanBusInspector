// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class ChargeDischargeMessage : IChargeDischargeMessage
    {
        public CarType CarType => CarType.Model3;
        public ushort MessageTypeId => 0x3D2;
        public byte RequireBytes => 8;

        public KiloWattHour TotalDischarge { get; }
        public KiloWattHour TotalCharge { get; }

        internal ChargeDischargeMessage()
        {
        }

        public ChargeDischargeMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            TotalDischarge = new KiloWattHour(BitArrayConverter.ToUInt32(payload, 0, 32) * 0.001m);
            TotalCharge = new KiloWattHour(BitArrayConverter.ToUInt32(payload, 32, 32) * 0.001m);
        }
    }

    public interface IChargeDischargeMessage : ICanBusMessage
    {
        KiloWattHour TotalDischarge { get; }
        KiloWattHour TotalCharge { get; }
    }
}