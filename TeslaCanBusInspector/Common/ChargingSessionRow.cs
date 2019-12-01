using System;
using TeslaCanBusInspector.Common.ValueTypes;

namespace TeslaCanBusInspector.Common
{
    public class ChargingSessionRow
    {
        public DateTime Timestamp;

        public Ampere? BatteryCurrent;
        public Volt? BatteryVoltage;
        public Percent? StateOfCharge;
    }
}
