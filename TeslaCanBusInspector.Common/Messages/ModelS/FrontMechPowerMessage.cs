using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class FrontMechPowerMessage : IFrontMechPowerMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;

        public const ushort TypeId = 0x2E5;
        public ushort MessageTypeId => TypeId;

        public KiloWatt FrontMechPower { get; }
        public KiloWatt FrontDissipation { get; }
        public KiloWatt FrontInputPower { get; }
        public Ampere FrontStatorCurrent { get; }
        public KiloWatt FrontDriveMaxPower { get; }

        internal FrontMechPowerMessage()
        {
        }

        public FrontMechPowerMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            FrontMechPower = new KiloWatt((payload[2] + ((payload[3] & 0x7) << 8) - 512 * (payload[3] & 0x4)) / 2m);
            FrontDissipation = new KiloWatt(payload[1] * 125m / 1000m - 0.5m);
            FrontInputPower = new KiloWatt(FrontMechPower + FrontDissipation);
            FrontStatorCurrent = new Ampere(payload[4] + ((payload[5] & 0x7) << 8));
            FrontDriveMaxPower = new KiloWatt((payload[6] & 0x3f << 5) + ((payload[5] & 0xf0) >> 3) + 1m);
        }
    }

    public interface IFrontMechPowerMessage : ICanBusMessage
    {
        KiloWatt FrontMechPower { get; }
        KiloWatt FrontDissipation { get; }
        KiloWatt FrontInputPower { get; }
        Ampere FrontStatorCurrent { get; }
        KiloWatt FrontDriveMaxPower{ get; }
    }
}