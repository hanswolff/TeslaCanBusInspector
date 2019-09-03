using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Volt : IEquatable<Volt>
    {
        public readonly decimal Value;

        public Volt(int value)
        {
            Value = value;
        }

        public Volt(decimal value)
        {
            Value = value;
        }

        public Volt(double value)
        {
            Value = (decimal)value;
        }

        public Volt(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Volt other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Volt value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Volt valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N3} V";
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