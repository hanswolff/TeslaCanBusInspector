using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct KilometersPerHour : IEquatable<KilometersPerHour>
    {
        public readonly decimal Value;

        public KilometersPerHour(int value)
        {
            Value = value;
        }

        public KilometersPerHour(decimal value)
        {
            Value = value;
        }

        public KilometersPerHour(double value)
        {
            Value = (decimal)value;
        }

        public KilometersPerHour(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(KilometersPerHour other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is KilometersPerHour && Equals((KilometersPerHour)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(KilometersPerHour valueType)
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