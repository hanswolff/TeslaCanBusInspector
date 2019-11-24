using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct WattHour : IEquatable<WattHour>
    {
        public readonly decimal Value;

        public WattHour(int value)
        {
            Value = value;
        }

        public WattHour(decimal value)
        {
            Value = value;
        }

        public WattHour(double value)
        {
            Value = (decimal)value;
        }

        public WattHour(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(WattHour other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is WattHour value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(WattHour valueType)
        {
            return valueType.Value;
        }

        public static implicit operator KiloWattHour(WattHour valueType)
        {
            return new KiloWattHour(valueType.Value / 1000m);
        }

        public override string ToString()
        {
            return $"{Value:N} Wh";
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return Value.ToString(formatProvider);
        }

        public string ToString(string format)
        {
            return Value.ToString(format);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return Value.ToString(format, formatProvider);
        }
    }
}