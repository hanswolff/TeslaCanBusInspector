using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages
{
    public class RearMotorRpmMessage : IRearMotorRpmMessage
    {
        public const ushort TypeId = 0x106;
        public ushort MessageTypeId => TypeId;

        public RevolutionsPerMinute RearRpm { get; }

        internal RearMotorRpmMessage()
        {
        }

        public RearMotorRpmMessage(byte[] payload)
        {
            payload.RequireBytes(6);

            RearRpm = new RevolutionsPerMinute(payload[4] + (payload[5] << 8) - 512 * (payload[5] & 0x80));
        }
    }

    public interface IRearMotorRpmMessage : ICanBusMessage
    {
        RevolutionsPerMinute RearRpm { get; }
    }
}