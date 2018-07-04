using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector.Common.Messages
{
    // ReSharper disable UnusedMember.Global
    public class SpeedMessage : ISpeedMessage
    {
        public const ushort TypeId = 0x256;
        public ushort MessageTypeId => TypeId;

        public KilometerPerHour Speed { get; }

        internal SpeedMessage()
        {
        }

        public SpeedMessage(byte[] payload)
        {
            // TODO: add code here
        }
    }

    public interface ISpeedMessage : ICanBusMessage
    {
        KilometerPerHour Speed { get; }
    }
}