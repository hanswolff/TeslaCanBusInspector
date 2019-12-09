using System;
using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector.Common
{
    public class ChargingSessionRow
    {
        public DateTime Timestamp;

        public Ampere? BatteryCurrent;
        public Volt? BatteryVoltage;
        public ushort? BmsChargeStatus;
        public ushort? BmsState;

        public Celsius? CellTemperature;

        public KiloWatt? BatteryPower => BatteryCurrent != null && BatteryVoltage != null
            ? new KiloWatt(BatteryCurrent.Value * BatteryVoltage.Value / 1000m)
            : (KiloWatt?)null;

        public KiloWatt? BatteryPowerAbs => BatteryPower != null
            ? new KiloWatt(Math.Abs(BatteryPower.Value))
            : (KiloWatt?) null;

        public KiloWatt? MaxChargePower;

        public Percent? StateOfCharge;

        public bool ShouldWriteRow =>
            BatteryCurrent != null &&
            BatteryVoltage != null &&
            CellTemperature != null &&
            MaxChargePower != null &&
            StateOfCharge != null;
    }
}
