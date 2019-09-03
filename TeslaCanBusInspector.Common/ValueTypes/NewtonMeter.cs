using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct NewtonMeter : IEquatable<NewtonMeter>
    {
        public readonly decimal Value;

        public NewtonMeter(int value)
        {
            Value = value;
        }

        public NewtonMeter(decimal value)
        {
            Value = value;
        }

        public NewtonMeter(double value)
        {
            Value = (decimal)value;
        }

        public NewtonMeter(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(NewtonMeter other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is NewtonMeter value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(NewtonMeter valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N1} Nm";
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