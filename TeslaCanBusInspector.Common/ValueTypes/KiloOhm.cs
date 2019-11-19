using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct KiloOhm : IEquatable<KiloOhm>
    {
        public readonly decimal Value;

        public KiloOhm(int value)
        {
            Value = value;
        }

        public KiloOhm(decimal value)
        {
            Value = value;
        }

        public KiloOhm(double value)
        {
            Value = (decimal)value;
        }

        public KiloOhm(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(KiloOhm other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is KiloOhm value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(KiloOhm valueType)
        {
            return valueType.Value;
        }

        public override string ToString()
        {
            return $"{Value:N2} kΩ";
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