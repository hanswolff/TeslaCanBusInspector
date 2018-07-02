using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Volts : IEquatable<Volts>
    {
        public readonly decimal Value;

        public Volts(int value)
        {
            Value = value;
        }

        public Volts(decimal value)
        {
            Value = value;
        }

        public Volts(double value)
        {
            Value = (decimal)value;
        }

        public Volts(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Volts other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Volts && Equals((Volts)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Volts valueType)
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