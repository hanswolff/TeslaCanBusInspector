using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class RearMotorRpmMessage : IRearMotorRpmMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x106;
        public byte RequireBytes => 6;

        public RevolutionsPerMinute RearRpm { get; }

        internal RearMotorRpmMessage()
        {
        }

        public RearMotorRpmMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            RearRpm = new RevolutionsPerMinute(payload[4] + (payload[5] << 8) - 512 * (payload[5] & 0x80));
        }
    }

    public interface IRearMotorRpmMessage : ICanBusMessage
    {
        RevolutionsPerMinute RearRpm { get; }
    }
}