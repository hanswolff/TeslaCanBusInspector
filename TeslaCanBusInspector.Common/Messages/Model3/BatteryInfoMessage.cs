using System;
using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class BatteryInfoMessage : IBatteryInfoMessage
    {
        public CarType CarType => CarType.Model3;

        public const ushort TypeId = 0x132;
        public ushort MessageTypeId => TypeId;

        public Ampere BatteryCurrentSmooth { get; }
        public Ampere BatteryCurrentRaw { get; }
        public TimeSpan ChargeTimeRemaining { get; }
        public Volt BatteryVoltage { get; }

        internal BatteryInfoMessage()
        {
        }

        public BatteryInfoMessage(byte[] payload)
        {
            payload.RequireBytes(8);
            BatteryVoltage = new Volt(BitArrayConverter.ToUInt16(payload, 0, 16) / 100.0m);
            BatteryCurrentSmooth = new Ampere(BitArrayConverter.ToInt16(payload, 16, 15) * -0.01m);
            BatteryCurrentRaw = new Ampere(BitArrayConverter.ToInt16(payload, 32, 16) * -0.05m + 500m);
            ChargeTimeRemaining = TimeSpan.FromMinutes(BitArrayConverter.ToUInt16(payload, 48, 12));
        }
    }

    public interface IBatteryInfoMessage : ICanBusMessage
    {
        Ampere BatteryCurrentSmooth { get; }
        Ampere BatteryCurrentRaw { get; }
        TimeSpan ChargeTimeRemaining { get; }
        Volt BatteryVoltage { get; }
    }
}