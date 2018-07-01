// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Models
{
    public class SteeringAngleMessage : ISteeringAngleMessage
    {
        public const ushort TypeId = 0x00E;
        public ushort MessageTypeId => TypeId;

        public decimal SteeringAngleDegrees { get; }

        internal SteeringAngleMessage()
        {
        }

        public SteeringAngleMessage(byte[] payload)
        {
            payload.RequireBytes(3);

            SteeringAngleDegrees = ((payload[0] << 8) + payload[1] - 8200.0m) / 10.0m;
        }
    }

    public interface ISteeringAngleMessage : ICanBusMessage
    {
        decimal SteeringAngleDegrees { get; }
    }
}