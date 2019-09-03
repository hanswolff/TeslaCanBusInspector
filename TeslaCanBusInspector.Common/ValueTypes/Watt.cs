using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Watt : IEquatable<Watt>
    {
        public readonly decimal Value;

        public Watt(int value)
        {
            Value = value;
        }

        public Watt(decimal value)
        {
            Value = value;
        }

        public Watt(double value)
        {
            Value = (decimal)value;
        }

        public Watt(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Watt other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Watt value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Watt valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N1} W";
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
