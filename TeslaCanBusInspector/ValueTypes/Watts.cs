using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Watts : IEquatable<Watts>
    {
        public readonly decimal Value;

        public Watts(int value)
        {
            Value = value;
        }

        public Watts(decimal value)
        {
            Value = value;
        }

        public Watts(double value)
        {
            Value = (decimal)value;
        }

        public Watts(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Watts other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Watts && Equals((Watts) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Watts valueType)
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
