using TeslaCanBusInspector.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Messages
{
    public class FrontMotorRpmMessage : IFrontMotorRpmMessage
    {
        public const ushort TypeId = 0x115;
        public ushort MessageTypeId => TypeId;

        public RevolutionsPerMinute FrontRpm { get; }

        internal FrontMotorRpmMessage()
        {
        }

        public FrontMotorRpmMessage(byte[] payload)
        {
            payload.RequireBytes(6);

            FrontRpm = new RevolutionsPerMinute(payload[4] + (payload[5] << 8) - 512 * (payload[5] & 0x80));
        }
    }

    public interface IFrontMotorRpmMessage : ICanBusMessage
    {
        RevolutionsPerMinute FrontRpm { get; }
    }
}