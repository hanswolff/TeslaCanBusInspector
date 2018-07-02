// ReSharper disable UnusedMember.Global

using System.Diagnostics.CodeAnalysis;
using TeslaCanBusInspector.ValueTypes;

namespace TeslaCanBusInspector.Models
{
    public class StateOfChargeMessage : IStateOfChargeMessage
    {
        public const ushort TypeId = 0x302;
        public ushort MessageTypeId => TypeId;

        public ChargeTotalType ChargeTotalType { get; set; }
        public KiloWattHour ChargeTotal { get; }
        public decimal StateOfChargeMin { get; }
        public decimal StateOfChargeDisplayed { get; }

        internal StateOfChargeMessage()
        {
        }

        public StateOfChargeMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            DetermineChargeTotalType(payload);
            ChargeTotal = new KiloWattHour((payload[4] + (payload[5] << 8) + (payload[6] << 16) + (payload[7] << 24)) / 1000.0m);
            StateOfChargeMin = (payload[0] + ((payload[1] & 0x3) << 8)) / 10m;
            StateOfChargeDisplayed = (payload[1] >> 2) + ((payload[2] & 0xF) << 6) / 10.0m;
        }

        private void DetermineChargeTotalType(byte[] payload)
        {
            switch (payload[2] >> 4)
            {
                case 0:
                    ChargeTotalType = ChargeTotalType.DC;
                    break;
                case 1:
                    ChargeTotalType = ChargeTotalType.AC;
                    break;
                default:
                    ChargeTotalType = ChargeTotalType.Unknown;
                    break;
            }
        }
    }

    public interface IStateOfChargeMessage : ICanBusMessage
    {
        ChargeTotalType ChargeTotalType { get; }
        KiloWattHour ChargeTotal { get; }
        decimal StateOfChargeMin { get; }
        decimal StateOfChargeDisplayed { get; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum ChargeTotalType
    {
        Unknown = 0,
        AC = 1,
        DC = 2
    }
}