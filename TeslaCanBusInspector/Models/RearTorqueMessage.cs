// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.ValueTypes;

namespace TeslaCanBusInspector.Models
{
    public class RearTorqueMessage : IRearTorqueMessage
    {
        public const ushort TypeId = 0x154;
        public ushort MessageTypeId => TypeId;

        public NewtonMeter RearTorque { get; }
        public decimal WattPedal { get; }

        internal RearTorqueMessage()
        {
        }

        public RearTorqueMessage(byte[] payload)
        {
            payload.RequireBytes(7);

            RearTorque = new NewtonMeter((payload[5] + ((payload[6] & 0x1F) << 8) - 512 * (payload[6] & 0x10)) * 0.25m);
            WattPedal = payload[3] * 0.4m;
        }
    }

    public interface IRearTorqueMessage : ICanBusMessage
    {
        NewtonMeter RearTorque { get; }
        decimal WattPedal { get; }
    }
}