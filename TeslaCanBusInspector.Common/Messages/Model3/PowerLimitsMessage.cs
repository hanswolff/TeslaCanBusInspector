using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class PowerLimitsMessage : IPowerLimitsMessage
    {
        public CarType CarType => CarType.Model3;
        public ushort MessageTypeId => 0x252;
        public byte RequireBytes => 8;

        public KiloWatt RegenPowerLimit { get; }
        public KiloWatt DischargePowerLimit { get; }

        internal PowerLimitsMessage()
        {
        }

        public PowerLimitsMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            RegenPowerLimit = new KiloWatt(BitArrayConverter.ToUInt16(payload, 0, 16) * 0.01m);
            DischargePowerLimit = new KiloWatt(BitArrayConverter.ToUInt16(payload, 16, 16) * 0.01m);
        }
    }

    public interface IPowerLimitsMessage : ICanBusMessage
    {
        KiloWatt RegenPowerLimit { get; }
        KiloWatt DischargePowerLimit { get; }
    }
}