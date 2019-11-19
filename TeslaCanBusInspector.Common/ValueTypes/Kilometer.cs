using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct Kilometer : IEquatable<Kilometer>, IEquatable<Mile>
    {
        public readonly decimal Value;

        public Kilometer(int value)
        {
            Value = value;
        }

        public Kilometer(decimal value)
        {
            Value = value;
        }

        public Kilometer(double value)
        {
            Value = (decimal)value;
        }

        public Kilometer(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(Kilometer other)
        {
            return Value == other.Value;
        }

        public bool Equals(Mile other)
        {
            return Equals((Kilometer) other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Kilometer value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Kilometer valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value} km";
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