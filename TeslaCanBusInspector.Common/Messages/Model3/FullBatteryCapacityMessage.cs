﻿// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class FullBatteryCapacityMessage : IFullBatteryCapacityMessage
    {
        public CarType CarType => CarType.Model3;

        public const ushort TypeId = 0x352;
        public ushort MessageTypeId => TypeId;

        public KiloWattHour FullBatteryCapacity { get; }
        public KiloWattHour RemainingBatteryCapacity { get; }
        public KiloWattHour ExpectedRemainingCapacity { get; }
        public KiloWattHour IdealRemaining { get; }
        public KiloWattHour ToCompleteCharge { get; }
        public KiloWattHour EnergyBuffer { get; }

        internal FullBatteryCapacityMessage()
        {
        }

        public FullBatteryCapacityMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            FullBatteryCapacity = new KiloWattHour(BitArrayConverter.ToUInt16(payload, 0, 10) * 0.1m);
            RemainingBatteryCapacity = new KiloWattHour(BitArrayConverter.ToUInt16(payload, 10, 10) * 0.1m);
            ExpectedRemainingCapacity = new KiloWattHour(BitArrayConverter.ToUInt16(payload, 20, 10) * 0.1m);
            IdealRemaining = new KiloWattHour(BitArrayConverter.ToUInt16(payload, 30, 10) * 0.1m);
            ToCompleteCharge = new KiloWattHour(BitArrayConverter.ToUInt16(payload, 40, 10) * 0.1m);
            EnergyBuffer = new KiloWattHour(BitArrayConverter.ToUInt16(payload, 50, 10) * 0.1m);
        }
    }

    public interface IFullBatteryCapacityMessage : ICanBusMessage
    {
        KiloWattHour FullBatteryCapacity { get; }
        KiloWattHour RemainingBatteryCapacity { get; }
        KiloWattHour ExpectedRemainingCapacity { get; }
        KiloWattHour IdealRemaining { get; }
        KiloWattHour ToCompleteCharge { get; }
        KiloWattHour EnergyBuffer { get; }
    }
}