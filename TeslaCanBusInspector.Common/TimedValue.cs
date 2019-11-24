using System;
using System.Diagnostics;

namespace TeslaCanBusInspector.Common
{
    [DebuggerDisplay("{" + nameof(Timestamp) + "}: {" + nameof(Value) + "}")]
    public class TimedValue<T>
    {
        public DateTime? Timestamp { get; set; }
        public T Value { get; }

        public TimedValue(DateTime? timestamp, T value)
        {
            Value = value;
            Timestamp = timestamp;
        }
    }
}
