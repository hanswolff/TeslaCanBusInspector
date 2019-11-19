using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct KilometerPerHour : IEquatable<KilometerPerHour>
    {
        public readonly decimal Value;

        public KilometerPerHour(int value)
        {
            Value = value;
        }

        public KilometerPerHour(decimal value)
        {
            Value = value;
        }

        public KilometerPerHour(double value)
        {
            Value = (decimal)value;
        }

        public KilometerPerHour(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(KilometerPerHour other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is KilometerPerHour value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(KilometerPerHour valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N} km/h";
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