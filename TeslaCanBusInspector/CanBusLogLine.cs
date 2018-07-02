namespace TeslaCanBusInspector
{
    public class CanBusLogLine
    {
        public readonly decimal UnixTimestamp;
        public readonly string CanInterface;
        public readonly ushort MessageTypeId;
        public readonly byte[] Payload;

        public CanBusLogLine(decimal unixTimestamp, string canInterface, ushort messageTypeId, byte[] payload)
        {
            UnixTimestamp = unixTimestamp;
            CanInterface = canInterface;
            MessageTypeId = messageTypeId;
            Payload = payload;
        }
    }
}