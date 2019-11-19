namespace TeslaCanBusInspector.Common.Messages
{
    public interface ICanBusMessage
    {
        CarType CarType { get; }
        ushort MessageTypeId { get; }
        byte RequireBytes { get; }
    }
}
