using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Kilometers : IEquatable<Kilometers>, IEquatable<Miles>
    {
        public readonly decimal Value;

        public Kilometers(int value)
        {
            Value = value;
        }

        public Kilometers(decimal value)
        {
            Value = value;
        }

        public Kilometers(double value)
        {
            Value = (decimal)value;
        }

        public Kilometers(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Kilometers other)
        {
            return Value == other.Value;
        }

        public bool Equals(Miles other)
        {
            return Equals((Kilometers) other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Kilometers && Equals((Kilometers)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Kilometers valueType)
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