namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public interface ICanBusMessage
    {
        ushort MessageTypeId { get; }
        CarType CarType { get; }
    }
}
