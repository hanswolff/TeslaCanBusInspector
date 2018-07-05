﻿using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages
{
    public class TemperatureMessage : ITemperatureMessage
    {
        public const ushort TypeId = 0x318;
        public ushort MessageTypeId => TypeId;

        public Celsius AirConditioningTemperature { get; }
        public Celsius InsideTemperature { get; }
        public Celsius OutsideTemperature { get; }
        public Celsius OutsideTemperatureFiltered { get; }

        internal TemperatureMessage()
        {
        }

        public TemperatureMessage(byte[] payload)
        {
            payload.RequireBytes(5);

            this.AirConditioningTemperature = new Celsius(payload[4] / 2.0m - 40m);
            this.InsideTemperature = new Celsius(payload[2] / 2.0m - 40m);
            this.OutsideTemperature = new Celsius(payload[0] / 2.0m - 40m);
            this.OutsideTemperatureFiltered = new Celsius(payload[1] / 2.0m - 40m);
        }
    }

    public interface ITemperatureMessage : ICanBusMessage
    {
        Celsius AirConditioningTemperature { get; }
        Celsius InsideTemperature { get; }
        Celsius OutsideTemperature { get; }
        Celsius OutsideTemperatureFiltered { get; }
    }
}