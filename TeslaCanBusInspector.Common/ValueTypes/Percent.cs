using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Percent : IEquatable<Percent>
    {
        public readonly decimal Value;

        public Percent(int value)
        {
            Value = value;
        }

        public Percent(decimal value)
        {
            Value = value;
        }

        public Percent(double value)
        {
            Value = (decimal)value;
        }

        public Percent(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Percent other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Percent value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Percent valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N2} %";
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