// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Models
{
    public class BatteryInfoMessage : IBatteryInfoMessage
    {
        public const ushort TypeId = 0x102;
        public ushort MessageTypeId => TypeId;

        
        public decimal BatteryCurrent { get; }
        public decimal BatteryPowerWatts { get; }
        public decimal BatteryVoltage { get; }
        public decimal NegativeTerminal { get; }

        internal BatteryInfoMessage()
        {
        }

        public BatteryInfoMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            BatteryCurrent = 1000 - ((((payload[3] & 0x7F) << 8) + payload[2]) << 1) / 20.0m;
            BatteryVoltage =  (payload[0] + (payload[1] << 8)) / 100.0m;
            BatteryPowerWatts = BatteryCurrent * BatteryVoltage;
            NegativeTerminal = (payload[6] + ((payload[7] & 0x07) << 8)) * 0.1m - 10m;
        }
    }

    public interface IBatteryInfoMessage : ICanBusMessage
    {
        decimal BatteryCurrent { get; }
        decimal BatteryPowerWatts { get; }
        decimal BatteryVoltage { get; }
        decimal NegativeTerminal { get; }
    }
}