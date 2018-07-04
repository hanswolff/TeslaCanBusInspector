// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector.Common.Messages
{
    public class OdometerMessage : IOdometerMessage
    {
        public const ushort TypeId = 0x562;
        public ushort MessageTypeId => TypeId;

        public Mile OdometerValue { get; }

        internal OdometerMessage()
        {
        }

        public OdometerMessage(byte[] payload)
        {
            payload.RequireBytes(4);

            OdometerValue = new Mile((payload[0] + (payload[1] << 8) + (payload[2] << 16) + (payload[3] << 24)) / 1000.0m);
        }
    }

    public interface IOdometerMessage : ICanBusMessage
    {
        Mile OdometerValue { get; }
    }
}