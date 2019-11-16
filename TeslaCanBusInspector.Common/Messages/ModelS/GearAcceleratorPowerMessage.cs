// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class GearAcceleratorPowerMessage : ICanBusMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;

        public const ushort TypeId = 0x254;
        public ushort MessageTypeId => TypeId;

        internal GearAcceleratorPowerMessage()
        {
        }

        public GearAcceleratorPowerMessage(byte[] payload)
        {
            // TODO: add code here
        }
    }
}