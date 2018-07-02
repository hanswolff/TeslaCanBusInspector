using TeslaCanBusInspector.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Models
{
    public class RearTorqueMessage : IRearTorqueMessage
    {
        public const ushort TypeId = 0x154;
        public ushort MessageTypeId => TypeId;

        public NewtonMeter RearTorque { get; }
        public Percent WattPedal { get; }

        internal RearTorqueMessage()
        {
        }

        public RearTorqueMessage(byte[] payload)
        {
            payload.RequireBytes(7);

            RearTorque = new NewtonMeter((payload[5] + ((payload[6] & 0x1F) << 8) - 512 * (payload[6] & 0x10)) * 0.25m);
            WattPedal = new Percent(payload[3] * 0.4m);
        }
    }

    public interface IRearTorqueMessage : ICanBusMessage
    {
        NewtonMeter RearTorque { get; }
        Percent WattPedal { get; }
    }
}