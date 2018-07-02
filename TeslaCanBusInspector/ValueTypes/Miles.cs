using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Miles : IEquatable<Miles>, IEquatable<Kilometers>
    {
        public const decimal MilesToKm = 1.609344m;

        public readonly decimal Value;

        public Miles(int value)
        {
            Value = value;
        }

        public Miles(decimal value)
        {
            Value = value;
        }

        public Miles(double value)
        {
            Value = (decimal)value;
        }

        public Miles(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Miles other)
        {
            return Value == other.Value;
        }

        public bool Equals(Kilometers other)
        {
            return Equals((Miles) other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Miles && Equals((Miles)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static explicit operator Miles(Kilometers kilometers)
        {
            return new Miles(kilometers / MilesToKm);
        }

        public static explicit operator Kilometers(Miles miles)
        {
            return new Kilometers(miles * MilesToKm);
        }

        public static implicit operator decimal(Miles valueType)
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