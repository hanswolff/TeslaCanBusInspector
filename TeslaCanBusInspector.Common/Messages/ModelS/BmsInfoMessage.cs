// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class BmsInfoMessage : IBmsInfoMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x232;
        public byte RequireBytes => 4;

        public KiloWatt BmsMaxCharge { get; }
        public KiloWatt BmsMaxDischarge { get; }

        internal BmsInfoMessage()
        {
        }

        public BmsInfoMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            BmsMaxCharge = new KiloWatt((payload[0] + (payload[1] << 8)) / 100.0m);
            BmsMaxDischarge = new KiloWatt((payload[2] + (payload[3] << 8)) / 100.0m);
        }
    }

    public interface IBmsInfoMessage : ICanBusMessage
    {
        KiloWatt BmsMaxCharge { get; }
        KiloWatt BmsMaxDischarge { get; }
    }
}