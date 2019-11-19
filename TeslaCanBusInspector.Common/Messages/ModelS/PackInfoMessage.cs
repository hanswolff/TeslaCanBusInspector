using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class PackInfoMessage : IPackInfoMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x382;
        public byte RequireBytes => 8;

        public KiloWattHour EnergyBuffer { get; }
        public KiloWattHour ExpectedRemaining { get; }
        public KiloWattHour IdealRemaining { get; }
        public KiloWattHour NominalFullPack { get; }
        public KiloWattHour NominalRemaining { get; }
        public KiloWattHour ToChargeComplete { get; }

        internal PackInfoMessage()
        {
        }

        public PackInfoMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            EnergyBuffer = new KiloWattHour(((payload[6] >> 2) + (payload[7] & 0x03) * 64) * 0.1m);
            ExpectedRemaining = new KiloWattHour(((payload[2] >> 4) + (payload[3] & 0x3F) << 4) * 0.1m);
            IdealRemaining = new KiloWattHour(((payload[3] >> 6) + (payload[4] & 0xFF) << 2) * 0.1m);
            NominalFullPack = new KiloWattHour((payload[0] + ((payload[1] & 0x03) << 8)) * 0.1m);
            NominalRemaining = new KiloWattHour(((payload[1] >> 2) + ((payload[2] & 0x0F) << 6)) * 0.1m);
            ToChargeComplete = new KiloWattHour((payload[5] + ((payload[6] & 0x03) << 8)) * 0.1);
        }
    }

    public interface IPackInfoMessage
    {
        KiloWattHour EnergyBuffer { get; }
        KiloWattHour ExpectedRemaining { get; }
        KiloWattHour IdealRemaining { get; }
        KiloWattHour NominalFullPack { get; }
        KiloWattHour NominalRemaining { get; }
        KiloWattHour ToChargeComplete { get; }
    }
}