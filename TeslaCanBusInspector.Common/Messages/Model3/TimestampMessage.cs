using System;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class TimestampMessage : ITimestampMessage
    {
        public CarType CarType => CarType.Model3;

        public const ushort TypeId = 0x318;
        public ushort MessageTypeId => TypeId;

        public DateTime Timestamp { get; }

        internal TimestampMessage()
        {
        }

        public TimestampMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            Timestamp = new DateTime(2000 + payload[0], payload[1], payload[4], payload[3], payload[5], payload[2], DateTimeKind.Utc);
        }
    }

    public interface ITimestampMessage : ICanBusMessage
    {
        DateTime Timestamp { get; }
    }
}