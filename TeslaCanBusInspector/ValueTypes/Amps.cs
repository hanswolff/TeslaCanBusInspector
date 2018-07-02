using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Amps : IEquatable<Amps>
    {
        public readonly decimal Value;

        public Amps(int value)
        {
            Value = value;
        }

        public Amps(decimal value)
        {
            Value = value;
        }

        public Amps(double value)
        {
            Value = (decimal)value;
        }

        public Amps(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Amps other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Amps && Equals((Amps)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Amps valueType)
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