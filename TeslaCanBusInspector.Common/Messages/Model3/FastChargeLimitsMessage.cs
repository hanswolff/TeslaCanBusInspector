using System;
using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class FastChargeLimitsMessage : IFastChargeLimitsMessage
    {
        public CarType CarType => CarType.Model3;
        public ushort MessageTypeId => 0x244;
        public byte RequireBytes => 8;

        public KiloWatt FastChargePowerLimit { get; }
        public Ampere FastChargeCurrentLimit { get; }
        public Volt FastChargeMaxVoltage { get; }
        public Volt FastChargeMinVoltage { get; }

        internal FastChargeLimitsMessage()
        {
        }

        public FastChargeLimitsMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            FastChargePowerLimit = new KiloWatt(Math.Round(BitArrayConverter.ToUInt16(payload, 0, 13) * 0.06226m, 4));
            FastChargeCurrentLimit = new Ampere(Math.Round(BitArrayConverter.ToUInt16(payload, 16, 13) * 0.073242m, 2));
            FastChargeMaxVoltage = new Volt(Math.Round(BitArrayConverter.ToUInt16(payload, 32, 13) * 0.073242m, 2));
            FastChargeMinVoltage = new Volt(Math.Round(BitArrayConverter.ToUInt16(payload, 48, 13) * 0.073242m, 2));
        }
    }

    public interface IFastChargeLimitsMessage : ICanBusMessage
    {
        KiloWatt FastChargePowerLimit { get; }
        Ampere FastChargeCurrentLimit { get; }
        Volt FastChargeMaxVoltage { get; }
        Volt FastChargeMinVoltage { get; }
    }
}