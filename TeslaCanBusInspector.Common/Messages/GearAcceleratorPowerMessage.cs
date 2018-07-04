// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages
{
    public class GearAcceleratorPowerMessage : ICanBusMessage
    {
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