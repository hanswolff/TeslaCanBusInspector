using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct KiloWattHours : IEquatable<KiloWattHours>
    {
        public readonly decimal Value;

        public KiloWattHours(int value)
        {
            Value = value;
        }

        public KiloWattHours(decimal value)
        {
            Value = value;
        }

        public KiloWattHours(double value)
        {
            Value = (decimal)value;
        }

        public KiloWattHours(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(KiloWattHours other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is KiloWattHours && Equals((KiloWattHours)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(KiloWattHours valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
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