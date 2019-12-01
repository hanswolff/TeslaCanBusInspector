using System;
using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector.Common
{
    public class CsvRow
    {
        public DateTime Timestamp;

        public ushort? BmsState;
        public ushort? BmsChargeStatus;

        public Ampere? BatteryCurrent;
        public Volt? BatteryVoltage;

        public KiloWattHour? FullBatteryCapacity;
        public KiloWattHour? ExpectedRemainingCapacity;
        public KiloWattHour? TotalCharge;
        public KiloWattHour? TotalDischarge;

        public Percent? StateOfCharge;

        public KilometerPerHour? Speed;
        public Kilometer? Odometer;
    }
}
