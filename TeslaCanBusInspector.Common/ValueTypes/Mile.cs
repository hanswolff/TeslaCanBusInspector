using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Mile : IEquatable<Mile>, IEquatable<Kilometer>
    {
        public const decimal MilesToKm = 1.609344m;

        public readonly decimal Value;

        public Mile(int value)
        {
            Value = value;
        }

        public Mile(decimal value)
        {
            Value = value;
        }

        public Mile(double value)
        {
            Value = (decimal)value;
        }

        public Mile(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Mile other)
        {
            return Value == other.Value;
        }

        public bool Equals(Kilometer other)
        {
            return Equals((Mile) other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Mile value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static explicit operator Mile(Kilometer kilometer)
        {
            return new Mile(kilometer / MilesToKm);
        }

        public static explicit operator Kilometer(Mile mile)
        {
            return new Kilometer(mile * MilesToKm);
        }

        public static implicit operator decimal(Mile valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N2} miles";
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