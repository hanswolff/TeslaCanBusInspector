namespace TeslaCanBusInspector.Models
{
    public class SpeedMessage : ICanBusMessage
    {
        public const ushort TypeId = 0x256;
        public ushort MessageTypeId => TypeId;

        internal SpeedMessage()
        {
        }

        public SpeedMessage(byte[] payload)
        {
            // TODO: add code here
        }
    }
}