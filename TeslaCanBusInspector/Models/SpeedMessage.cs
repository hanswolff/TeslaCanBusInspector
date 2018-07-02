using TeslaCanBusInspector.ValueTypes;

namespace TeslaCanBusInspector.Models
{
    // ReSharper disable UnusedMember.Global
    public class SpeedMessage : ISpeedMessage
    {
        public const ushort TypeId = 0x256;
        public ushort MessageTypeId => TypeId;

        public KilometersPerHour Speed { get; }

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
        KilometersPerHour Speed { get; }
    }
}