using System;
using TeslaCanBusInspector.Common.Messages.ModelS;
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
            BatteryVoltage = new Volt(BitConverter.ToUInt16(payload, 0) / 100.0m);
            BatteryCurrentSmooth = new Ampere(BitConverter.ToInt16(payload, 2) * -0.01m);
            BatteryCurrentRaw = new Ampere(BitConverter.ToInt16(payload, 4) * -0.05m + 500m);
            ChargeTimeRemaining = TimeSpan.FromMinutes(payload[7] << 8 + payload[6]);
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