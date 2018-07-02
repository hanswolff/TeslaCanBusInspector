namespace TeslaCanBusInspector.Messages
{
    public interface ICanBusMessage
    {
        ushort MessageTypeId { get; }
    }
}
