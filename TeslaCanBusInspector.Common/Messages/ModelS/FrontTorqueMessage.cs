using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class FrontTorqueMessage : IFrontTorqueMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x1D4;
        public byte RequireBytes => 7;

        public NewtonMeter FrontTorque { get; }

        internal FrontTorqueMessage()
        {
        }

        public FrontTorqueMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            FrontTorque = new NewtonMeter((payload[5] + ((payload[6] & 0x1F) << 8) - 512 * (payload[6] & 0x10)) * 0.25m);
        }
    }

    public interface IFrontTorqueMessage : ICanBusMessage
    {
        NewtonMeter FrontTorque { get; }
    }
}