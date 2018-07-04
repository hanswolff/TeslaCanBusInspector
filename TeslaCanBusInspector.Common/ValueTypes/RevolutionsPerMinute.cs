using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct RevolutionsPerMinute : IEquatable<RevolutionsPerMinute>
    {
        public readonly decimal Value;

        public RevolutionsPerMinute(int value)
        {
            Value = value;
        }

        public RevolutionsPerMinute(decimal value)
        {
            Value = value;
        }

        public RevolutionsPerMinute(double value)
        {
            Value = (decimal)value;
        }

        public RevolutionsPerMinute(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(RevolutionsPerMinute other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is RevolutionsPerMinute && Equals((RevolutionsPerMinute)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(RevolutionsPerMinute valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N0} rpm";
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