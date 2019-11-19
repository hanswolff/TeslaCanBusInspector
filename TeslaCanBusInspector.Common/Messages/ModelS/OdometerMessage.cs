// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class OdometerMessage : IOdometerMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x562;
        public byte RequireBytes => 4;

        public Mile OdometerValue { get; }

        internal OdometerMessage()
        {
        }

        public OdometerMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            OdometerValue = new Mile((payload[0] + (payload[1] << 8) + (payload[2] << 16) + (payload[3] << 24)) / 1000.0m);
        }
    }

    public interface IOdometerMessage : ICanBusMessage
    {
        Mile OdometerValue { get; }
    }
}