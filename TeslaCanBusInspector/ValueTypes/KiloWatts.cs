using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct KiloWatts : IEquatable<KiloWatts>
    {
        public readonly decimal Value;

        public KiloWatts(int value)
        {
            Value = value;
        }

        public KiloWatts(decimal value)
        {
            Value = value;
        }

        public KiloWatts(double value)
        {
            Value = (decimal)value;
        }

        public KiloWatts(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(KiloWatts other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is KiloWatts && Equals((KiloWatts)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static explicit operator KiloWatts(Watts watts)
        {
            return new KiloWatts(watts / 1000m);
        }

        public static explicit operator Watts(KiloWatts kiloWatts)
        {
            return new Watts(kiloWatts * 1000m);
        }

        public static implicit operator decimal(KiloWatts valueType)
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