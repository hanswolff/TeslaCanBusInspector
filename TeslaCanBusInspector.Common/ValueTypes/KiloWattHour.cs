﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCanBusInspector.Common.ValueTypes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public struct KiloWattHour : IEquatable<KiloWattHour>
    {
        public readonly decimal Value;

        public KiloWattHour(int value)
        {
            Value = value;
        }

        public KiloWattHour(decimal value)
        {
            Value = value;
        }

        public KiloWattHour(double value)
        {
            Value = (decimal)value;
        }

        public KiloWattHour(float value)
        {
            Value = (decimal)value;
        }

        public bool Equals(KiloWattHour other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is KiloWattHour value && Equals(value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(KiloWattHour valueType)
        {
            return valueType.Value;
        }

        public static implicit operator WattHour(KiloWattHour valueType)
        {
            return new WattHour(valueType.Value * 1000m);
        }

        public override string ToString()
        {
            return $"{Value:N} kWh";
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