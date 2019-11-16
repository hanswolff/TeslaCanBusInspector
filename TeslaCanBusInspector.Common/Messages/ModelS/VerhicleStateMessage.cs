// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class VerhicleStateMessage : ICanBusMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;

        public const ushort TypeId = 0x116;
        public ushort MessageTypeId => TypeId;

        internal VerhicleStateMessage()
        {
        }

        public VerhicleStateMessage(byte[] payload)
        {
            // TODO: add code here
        }
    }
}