using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Celsius : IEquatable<Celsius>
    {
        public readonly decimal Value;

        public Celsius(int value)
        {
            Value = value;
        }

        public Celsius(decimal value)
        {
            Value = value;
        }

        public Celsius(double value)
        {
            Value = (decimal)value;
        }

        public Celsius(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Celsius other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Celsius value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Celsius valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N} °C";
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