using System;

namespace TeslaCanBusInspector.Configuration
{
    public class ChargingSessionTransformationOptions
    {
        public TimeSpan MinimumChargingSessionDuration { get; set; }
        public bool IncludeSubdirectories { get; set; }
    }
}
