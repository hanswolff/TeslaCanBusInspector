using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class BatteryInfoMessage : IBatteryInfoMessage
    {
        public CarType CarType => CarType.Model3;
        public ushort MessageTypeId => 0x212;
        public byte RequireBytes => 8;

        public ushort BmsNumberOfContactors { get; }
        public ushort BmsState { get; }
        public KiloOhm IsolationResistance { get; }
        public ushort BmsChargeStatus { get; }
        public KiloWatt BmsChargePowerAvailable { get; }
        public Celsius MinBatteryTemperature { get; }

        internal BatteryInfoMessage()
        {
        }

        public BatteryInfoMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            BmsNumberOfContactors = BitArrayConverter.ToUInt16(payload, 8, 3);
            BmsState = BitArrayConverter.ToUInt16(payload, 11, 4);
            IsolationResistance = new KiloOhm(BitArrayConverter.ToInt16(payload, 19, 10));
            BmsChargeStatus = BitArrayConverter.ToUInt16(payload, 32, 3);
            BmsChargePowerAvailable = new KiloWatt(BitArrayConverter.ToUInt16(payload, 38, 11) * 0.125m);
            MinBatteryTemperature = new Celsius(BitArrayConverter.ToUInt16(payload, 56, 8) * 0.5m - 40m);
        }
    }

    public interface IBatteryInfoMessage : ICanBusMessage
    {
        ushort BmsNumberOfContactors { get; }
        ushort BmsState { get; }
        KiloOhm IsolationResistance { get; }
        ushort BmsChargeStatus { get; }
        KiloWatt BmsChargePowerAvailable { get; }
        Celsius MinBatteryTemperature { get; }
    }
}