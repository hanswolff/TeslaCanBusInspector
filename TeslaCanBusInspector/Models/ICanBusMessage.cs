namespace TeslaCanBusInspector.Models
{
    public interface ICanBusMessage
    {
        ushort MessageTypeId { get; }
    }
}
