using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class TemperatureMessage : ITemperatureMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x318;
        public byte RequireBytes => 5;

        public Celsius AirConditioningTemperature { get; }
        public Celsius InsideTemperature { get; }
        public Celsius OutsideTemperature { get; }
        public Celsius OutsideTemperatureFiltered { get; }

        internal TemperatureMessage()
        {
        }

        public TemperatureMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            AirConditioningTemperature = new Celsius(payload[4] / 2.0m - 40m);
            InsideTemperature = new Celsius(payload[2] / 2.0m - 40m);
            OutsideTemperature = new Celsius(payload[0] / 2.0m - 40m);
            OutsideTemperatureFiltered = new Celsius(payload[1] / 2.0m - 40m);
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