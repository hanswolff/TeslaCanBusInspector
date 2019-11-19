using System;
using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector.Common
{
    public class CsvRow
    {
        public DateTime Timestamp;

        public Ampere? BatteryCurrent;
        public Volt? BatteryVoltage;

        public Percent? StateOfCharge;

        public KilometerPerHour? Speed;
        public Kilometer? Odometer;
    }
}
