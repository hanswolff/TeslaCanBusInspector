using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct KiloWatt : IEquatable<KiloWatt>
    {
        public readonly decimal Value;

        public KiloWatt(int value)
        {
            Value = value;
        }

        public KiloWatt(decimal value)
        {
            Value = value;
        }

        public KiloWatt(double value)
        {
            Value = (decimal)value;
        }

        public KiloWatt(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(KiloWatt other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is KiloWatt value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static explicit operator KiloWatt(Watt watt)
        {
            return new KiloWatt(watt / 1000m);
        }

        public static explicit operator Watt(KiloWatt kiloWatt)
        {
            return new Watt(kiloWatt * 1000m);
        }

        public static implicit operator decimal(KiloWatt valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N3} kW";
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