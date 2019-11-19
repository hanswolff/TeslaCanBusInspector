// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class SteeringAngleMessage : ISteeringAngleMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x00E;
        public byte RequireBytes => 3;

        public decimal SteeringAngleDegrees { get; }

        internal SteeringAngleMessage()
        {
        }

        public SteeringAngleMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            SteeringAngleDegrees = ((payload[0] << 8) + payload[1] - 8200.0m) / 10.0m;
        }
    }

    public interface ISteeringAngleMessage : ICanBusMessage
    {
        decimal SteeringAngleDegrees { get; }
    }
}