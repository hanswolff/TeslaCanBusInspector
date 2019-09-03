using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Ampere : IEquatable<Ampere>
    {
        public readonly decimal Value;

        public Ampere(int value)
        {
            Value = value;
        }

        public Ampere(decimal value)
        {
            Value = value;
        }

        public Ampere(double value)
        {
            Value = (decimal)value;
        }

        public Ampere(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Ampere other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Ampere value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Ampere valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N2} A";
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