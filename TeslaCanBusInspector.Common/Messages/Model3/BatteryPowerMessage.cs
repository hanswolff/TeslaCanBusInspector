using System;
using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class BatteryPowerMessage : IBatteryPowerMessage
    {
        public CarType CarType => CarType.Model3;
        public ushort MessageTypeId => 0x132;
        public byte RequireBytes => 8;

        public Ampere BatteryCurrentSmooth { get; }
        public Ampere BatteryCurrentRaw { get; }
        public TimeSpan ChargeTimeRemaining { get; }
        public Volt BatteryVoltage { get; }

        internal BatteryPowerMessage()
        {
        }

        public BatteryPowerMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            BatteryVoltage = new Volt(BitArrayConverter.ToUInt16(payload, 0, 16) / 100.0m);
            BatteryCurrentSmooth = new Ampere(BitArrayConverter.ToInt16(payload, 16, 15) * -0.01m);
            BatteryCurrentRaw = new Ampere(BitArrayConverter.ToInt16(payload, 32, 16) * -0.05m + 500m);
            ChargeTimeRemaining = TimeSpan.FromMinutes(BitArrayConverter.ToUInt16(payload, 48, 12));
        }
    }

    public interface IBatteryPowerMessage : ICanBusMessage
    {
        Ampere BatteryCurrentSmooth { get; }
        Ampere BatteryCurrentRaw { get; }
        TimeSpan ChargeTimeRemaining { get; }
        Volt BatteryVoltage { get; }
    }
}