// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.ValueTypes;

namespace TeslaCanBusInspector.Models
{
    public class BmsInfoMessage : IBmsInfoMessage
    {
        public const ushort TypeId = 0x232;
        public ushort MessageTypeId => TypeId;

        public KiloWatt BmsMaxCharge { get; }
        public KiloWatt BmsMaxDischarge { get; }

        internal BmsInfoMessage()
        {
        }

        public BmsInfoMessage(byte[] payload)
        {
            payload.RequireBytes(4);

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