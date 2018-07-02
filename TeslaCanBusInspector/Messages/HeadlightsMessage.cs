// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Messages
{
    public class HeadlightsMessage : ICanBusMessage
    {
        public const ushort TypeId = 0x266;
        public ushort MessageTypeId => TypeId;

        internal HeadlightsMessage()
        {
        }

        public HeadlightsMessage(byte[] payload)
        {
            // TODO: add code here
        }
    }
}
