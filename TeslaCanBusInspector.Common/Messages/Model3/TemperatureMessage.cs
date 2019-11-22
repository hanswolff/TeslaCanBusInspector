using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class TemperatureMessage : ITemperatureMessage
    {
        public CarType CarType => CarType.Model3;
        public ushort MessageTypeId => 0x321;
        public byte RequireBytes => 8;

        public Celsius CoolantTempBatteryInlet { get; }
        public Celsius CoolantTempPowerTrainInlet { get; }
        public Celsius AmbientTempRaw { get; }
        public Celsius AmbientTempFiltered { get; }

        internal TemperatureMessage()
        {
        }

        public TemperatureMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            CoolantTempBatteryInlet = new Celsius(BitArrayConverter.ToUInt16(payload, 0, 10) * 0.125m - 40m);
            CoolantTempPowerTrainInlet = new Celsius(BitArrayConverter.ToUInt16(payload, 10, 10) * 0.125m - 40m);
            AmbientTempRaw = new Celsius(BitArrayConverter.ToUInt16(payload, 24, 8) * 0.5m - 40m);
            AmbientTempFiltered = new Celsius(BitArrayConverter.ToUInt16(payload, 40, 8) * 0.5m - 40m);
        }
    }

    public interface ITemperatureMessage : ICanBusMessage
    {
        Celsius CoolantTempBatteryInlet { get; }
        Celsius CoolantTempPowerTrainInlet { get; }
        Celsius AmbientTempRaw { get; }
        Celsius AmbientTempFiltered { get; }
    }
}