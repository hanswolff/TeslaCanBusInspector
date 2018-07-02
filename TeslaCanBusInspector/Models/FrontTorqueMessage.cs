// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.ValueTypes;

namespace TeslaCanBusInspector.Models
{
    public class FrontTorqueMessage : IFrontTorqueMessage
    {
        public const ushort TypeId = 0x1D4;
        public ushort MessageTypeId => TypeId;

        public NewtonMeter FrontTorque { get; }

        internal FrontTorqueMessage()
        {
        }

        public FrontTorqueMessage(byte[] payload)
        {
            payload.RequireBytes(7);

            FrontTorque = new NewtonMeter((payload[5] + ((payload[6] & 0x1F) << 8) - 512 * (payload[6] & 0x10)) * 0.25m);
        }
    }

    public interface IFrontTorqueMessage : ICanBusMessage
    {
        NewtonMeter FrontTorque { get; }
    }
}