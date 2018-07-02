// ReSharper disable UnusedMember.Global

using System.Diagnostics.CodeAnalysis;
using TeslaCanBusInspector.ValueTypes;

namespace TeslaCanBusInspector.Messages
{
    public class StateOfChargeMessage : IStateOfChargeMessage
    {
        public const ushort TypeId = 0x302;
        public ushort MessageTypeId => TypeId;

        public ChargeTotalType ChargeTotalType { get; set; }
        public KiloWattHour ChargeTotal { get; }
        public Percent StateOfChargeMin { get; }
        public Percent StateOfChargeDisplayed { get; }

        internal StateOfChargeMessage()
        {
        }

        public StateOfChargeMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            DetermineChargeTotalType(payload);
            ChargeTotal = new KiloWattHour((payload[4] + (payload[5] << 8) + (payload[6] << 16) + (payload[7] << 24)) / 1000.0m);
            StateOfChargeMin = new Percent((payload[0] + ((payload[1] & 0x3) << 8)) / 10m);
            StateOfChargeDisplayed = new Percent((payload[1] >> 2) + ((payload[2] & 0xF) << 6) / 10.0m);
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
        Percent StateOfChargeMin { get; }
        Percent StateOfChargeDisplayed { get; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum ChargeTotalType
    {
        Unknown = 0,
        AC = 1,
        DC = 2
    }
}