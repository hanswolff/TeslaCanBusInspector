// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.ValueTypes;

namespace TeslaCanBusInspector.Models
{
    public class BatteryInfoMessage : IBatteryInfoMessage
    {
        public const ushort TypeId = 0x102;
        public ushort MessageTypeId => TypeId;

        public Amps BatteryCurrent { get; }
        public Watts BatteryPower { get; }
        public Volts BatteryVoltage { get; }
        public decimal NegativeTerminal { get; }

        internal BatteryInfoMessage()
        {
        }

        public BatteryInfoMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            BatteryCurrent = new Amps(1000 - ((((payload[3] & 0x7F) << 8) + payload[2]) << 1) / 20.0m);
            BatteryVoltage = new Volts((payload[0] + (payload[1] << 8)) / 100.0m);
            BatteryPower = new Watts(BatteryCurrent * BatteryVoltage);
            NegativeTerminal = (payload[6] + ((payload[7] & 0x07) << 8)) * 0.1m - 10m;
        }
    }

    public interface IBatteryInfoMessage : ICanBusMessage
    {
        Amps BatteryCurrent { get; }
        Watts BatteryPower { get; }
        Volts BatteryVoltage { get; }
        decimal NegativeTerminal { get; }
    }
}