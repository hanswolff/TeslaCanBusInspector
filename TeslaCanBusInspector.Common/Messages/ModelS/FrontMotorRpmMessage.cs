using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class FrontMotorRpmMessage : IFrontMotorRpmMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x115;
        public byte RequireBytes => 6;

        public RevolutionsPerMinute FrontRpm { get; }

        internal FrontMotorRpmMessage()
        {
        }

        public FrontMotorRpmMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            FrontRpm = new RevolutionsPerMinute(payload[4] + (payload[5] << 8) - 512 * (payload[5] & 0x80));
        }
    }

    public interface IFrontMotorRpmMessage : ICanBusMessage
    {
        RevolutionsPerMinute FrontRpm { get; }
    }
}