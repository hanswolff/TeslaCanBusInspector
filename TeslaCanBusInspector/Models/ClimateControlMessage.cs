namespace TeslaCanBusInspector.Models
{
    public class ClimateControlMessage : ICanBusMessage
    {
        public const ushort TypeId = 0x268;
        public ushort MessageTypeId => TypeId;

        internal ClimateControlMessage()
        {
        }

        public ClimateControlMessage(byte[] payload)
        {
            // TODO: add code here
        }
    }
}