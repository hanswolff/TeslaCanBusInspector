// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class ClimateControlMessage : ICanBusMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;

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