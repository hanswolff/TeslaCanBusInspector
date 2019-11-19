using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct LitersPerMinute : IEquatable<LitersPerMinute>
    {
        public readonly decimal Value;

        public LitersPerMinute(int value)
        {
            Value = value;
        }

        public LitersPerMinute(decimal value)
        {
            Value = value;
        }

        public LitersPerMinute(double value)
        {
            Value = (decimal)value;
        }

        public LitersPerMinute(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(LitersPerMinute other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is LitersPerMinute value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(LitersPerMinute valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N} lpm";
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