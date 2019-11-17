// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class StateOfChargeMessage : IStateOfChargeMessage
    {
        public CarType CarType => CarType.Model3;

        public const ushort TypeId = 0x292;
        public ushort MessageTypeId => TypeId;

        public Percent StateOfChargeAvg { get; }
        public Percent StateOfChargeMax { get; }
        public Percent StateOfChargeMin { get; }
        public Percent StateOfChargeUI { get; }

        internal StateOfChargeMessage()
        {
        }

        public StateOfChargeMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            StateOfChargeUI = new Percent(BitArrayConverter.ToUInt16(payload, 0, 10) * 0.1m);
            StateOfChargeMin = new Percent(BitArrayConverter.ToUInt16(payload, 10, 10) * 0.1m);
            StateOfChargeMax = new Percent(BitArrayConverter.ToUInt16(payload, 20, 10) * 0.1m);
            StateOfChargeAvg = new Percent(BitArrayConverter.ToUInt16(payload, 30, 10) * 0.1m);
        }
    }

    public interface IStateOfChargeMessage : ICanBusMessage
    {
        Percent StateOfChargeAvg { get; }
        Percent StateOfChargeMax { get; }
        Percent StateOfChargeMin { get; }
        Percent StateOfChargeUI { get; }
    }
}