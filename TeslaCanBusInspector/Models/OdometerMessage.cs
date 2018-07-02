// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.ValueTypes;

namespace TeslaCanBusInspector.Models
{
    public class OdometerMessage : IOdometerMessage
    {
        public const ushort TypeId = 0x562;
        public ushort MessageTypeId => TypeId;

        public Miles OdometerValue { get; }

        internal OdometerMessage()
        {
        }

        public OdometerMessage(byte[] payload)
        {
            payload.RequireBytes(4);

            OdometerValue = new Miles((payload[0] + (payload[1] << 8) + (payload[2] << 16) + (payload[3] << 24)) / 1000.0m);
        }
    }

    public interface IOdometerMessage : ICanBusMessage
    {
        Miles OdometerValue { get; }
    }
}