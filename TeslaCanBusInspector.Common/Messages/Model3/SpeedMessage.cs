using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class SpeedMessage : ISpeedMessage
    {
        public CarType CarType => CarType.Model3;
        public ushort MessageTypeId => 0x257;
        public byte RequireBytes => 8;

        public KilometerPerHour SignedSpeed { get; }
        public KilometerPerHour UISpeed { get; }

        internal SpeedMessage()
        {
        }

        public SpeedMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);
            
            SignedSpeed = new KilometerPerHour(BitArrayConverter.ToUInt16(payload, 12, 12) * 0.08m - 40m);
            UISpeed = new KilometerPerHour(BitArrayConverter.ToUInt16(payload, 24, 8));
        }
    }

    public interface ISpeedMessage : ICanBusMessage
    {
        KilometerPerHour SignedSpeed { get; }
        KilometerPerHour UISpeed { get; }
    }
}