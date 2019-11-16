using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class RearMechPowerMessage : IRearMechPowerMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;

        public const ushort TypeId = 0x266;
        public ushort MessageTypeId => TypeId;

        public Volt RearInverterVoltage { get; }
        public KiloWatt RearMechPower { get; }
        public KiloWatt RearDissipation { get; }
        public KiloWatt RearInputPower { get; }
        public Ampere RearStatorCurrent { get; }
        public KiloWatt RearDriveMaxPower { get; }
        public KiloWatt RearRegenMaxPower { get; }

        internal RearMechPowerMessage()
        {
        }

        public RearMechPowerMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            RearInverterVoltage = new Volt(payload[0] / 10m);
            RearMechPower = new KiloWatt((payload[2] + ((payload[3] & 0x7) << 8) - 512 * (payload[3] & 0x4)) / 2m);
            RearDissipation = new KiloWatt(payload[1] * 125m / 1000m - 0.5m);
            RearInputPower = new KiloWatt(RearMechPower + RearDissipation);
            RearStatorCurrent = new Ampere(payload[4] + ((payload[5] & 0x7) << 8));
            RearDriveMaxPower = new KiloWatt((payload[6] & 0x3f << 5) + ((payload[5] & 0xf0) >> 3) + 1m);
            RearRegenMaxPower = new KiloWatt((payload[7] << 2) - 200m);
        }
    }

    public interface IRearMechPowerMessage : ICanBusMessage
    {
        Volt RearInverterVoltage { get; }
        KiloWatt RearMechPower { get; }
        KiloWatt RearDissipation { get; }
        KiloWatt RearInputPower { get; }
        Ampere RearStatorCurrent { get; }
        KiloWatt RearDriveMaxPower{ get; }
        KiloWatt RearRegenMaxPower{ get; }
    }
}