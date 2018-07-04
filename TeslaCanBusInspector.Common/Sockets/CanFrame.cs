namespace TeslaCanBusInspector.Common.Sockets
{
    public class CanFrame : ICanFrame
    {
        public bool StartOfFrame { get; set; }

        // TODO: add more stuff here
    }

    public interface ICanFrame
    {
        bool StartOfFrame { get; }

        // TODO: add more stuff here
    }
}