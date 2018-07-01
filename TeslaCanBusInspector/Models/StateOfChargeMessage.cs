// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Models
{
    public class StateOfChargeMessage : IStateOfChargeMessage
    {
        public const ushort TypeId = 0x302;
        public ushort MessageTypeId => TypeId;

        public decimal StateOfChargeMin { get; }
        public decimal StateOfChargeDisplayed { get; }

        internal StateOfChargeMessage()
        {
        }

        public StateOfChargeMessage(byte[] payload)
        {
            payload.RequireBytes(3);

            StateOfChargeMin = (payload[0] + ((payload[1] & 0x3) << 8)) / 10m;
            StateOfChargeDisplayed = (payload[1] >> 2) + ((payload[2] & 0xF) << 6) / 10.0m;
        }
    }

    public interface IStateOfChargeMessage : ICanBusMessage
    {
        decimal StateOfChargeMin { get; }

        decimal StateOfChargeDisplayed { get; }
    }
}