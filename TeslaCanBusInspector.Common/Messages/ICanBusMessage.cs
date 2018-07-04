namespace TeslaCanBusInspector.Common.Messages
{
    public interface ICanBusMessage
    {
        ushort MessageTypeId { get; }
    }
}
