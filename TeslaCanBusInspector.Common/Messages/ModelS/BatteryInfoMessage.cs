using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class BatteryInfoMessage : IBatteryInfoMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;

        public const ushort TypeId = 0x102;
        public ushort MessageTypeId => TypeId;

        public Ampere BatteryCurrent { get; }
        public Watt BatteryPower { get; }
        public Volt BatteryVoltage { get; }
        public decimal NegativeTerminal { get; }

        internal BatteryInfoMessage()
        {
        }

        public BatteryInfoMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            BatteryCurrent = new Ampere(1000 - ((((payload[3] & 0x7F) << 8) + payload[2]) << 1) / 20.0m);
            BatteryVoltage = new Volt((payload[0] + (payload[1] << 8)) / 100.0m);
            BatteryPower = new Watt(BatteryCurrent * BatteryVoltage);
            NegativeTerminal = (payload[6] + ((payload[7] & 0x07) << 8)) * 0.1m - 10m;
        }
    }

    public interface IBatteryInfoMessage : ICanBusMessage
    {
        Ampere BatteryCurrent { get; }
        Watt BatteryPower { get; }
        Volt BatteryVoltage { get; }
        decimal NegativeTerminal { get; }
    }
}