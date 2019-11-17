using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class FrontTorqueMessage : IFrontTorqueMessage
    {
        public CarType CarType => CarType.Model3;

        public const ushort TypeId = 0x186;
        public ushort MessageTypeId => TypeId;

        public NewtonMeter FrontTorqueRequest { get; }
        public NewtonMeter FrontTorque { get; }
        public RevolutionsPerMinute FrontAxleRpm { get; }

        internal FrontTorqueMessage()
        {
        }

        public FrontTorqueMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            FrontTorqueRequest = new NewtonMeter(BitArrayConverter.ToInt16(payload, 12, 13) * 2m);
            FrontTorque = new NewtonMeter(BitArrayConverter.ToInt16(payload, 27, 13) * 2m);
            FrontAxleRpm = new RevolutionsPerMinute(BitArrayConverter.ToInt16(payload, 40, 16) * 0.1m);
        }
    }

    public interface IFrontTorqueMessage : ICanBusMessage
    {
        NewtonMeter FrontTorqueRequest { get; }
        NewtonMeter FrontTorque { get; }
        RevolutionsPerMinute FrontAxleRpm { get; }
    }
}