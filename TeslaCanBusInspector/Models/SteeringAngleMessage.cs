using System;

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
            if (payload.Length < 2)
            {
                throw new ArgumentException($"{nameof(payload)} must have at least 2 bytes", nameof(payload));
            }

            SteeringAngleDegrees = ((payload[0] << 8) + payload[1] - 8200.0m) / 10.0m;
        }
    }

    public interface ISteeringAngleMessage : ICanBusMessage
    {
        decimal SteeringAngleDegrees { get; }
    }
}