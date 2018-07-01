using System;

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
            if (payload.Length < 3)
            {
                throw new ArgumentException($"{nameof(payload)} must have at least 3 bytes", nameof(payload));
            }

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