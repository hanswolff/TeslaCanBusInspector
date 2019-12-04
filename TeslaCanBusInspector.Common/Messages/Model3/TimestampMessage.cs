using System;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class TimestampMessage : ITimestampMessage
    {
        private static readonly DateTime MinTimestamp = new DateTime(2000, 1, 1);
        public const ushort MessageTypeIdConstant = 0x318;

        public CarType CarType => CarType.Model3;
        public ushort MessageTypeId => MessageTypeIdConstant;
        public byte RequireBytes => 8;

        public DateTime Timestamp { get; }
        public bool IsValid => Timestamp > MinTimestamp;

        internal TimestampMessage()
        {
        }

        public TimestampMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            try
            {
                if (payload[1] == 0) return;
                if (payload[4] == 0 || payload[4] > 31) return;

                Timestamp = new DateTime(2000 + payload[0], payload[1], payload[4], payload[3], payload[5], payload[2], DateTimeKind.Utc);
            }
            catch (ArgumentException)
            {
            }
        }
    }

    public interface ITimestampMessage : ICanBusMessage
    {
        DateTime Timestamp { get; }
        bool IsValid { get; }
    }
}