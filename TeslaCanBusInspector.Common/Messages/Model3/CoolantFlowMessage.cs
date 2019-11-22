using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class CoolantFlowMessage : ICoolantMessage
    {
        public CarType CarType => CarType.Model3;
        public ushort MessageTypeId => 0x241;
        public byte RequireBytes => 7;

        public LitersPerMinute BatteryCoolantFlowRate { get; }
        public LitersPerMinute PowerTrainCoolantFlowRate { get; }

        internal CoolantFlowMessage()
        {
        }

        public CoolantFlowMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            BatteryCoolantFlowRate = new LitersPerMinute(BitArrayConverter.ToUInt16(payload, 0, 9) * 0.1m);
            PowerTrainCoolantFlowRate = new LitersPerMinute(BitArrayConverter.ToUInt16(payload, 22, 9) * 0.1m);
        }
    }

    public interface ICoolantMessage : ICanBusMessage
    {
        LitersPerMinute BatteryCoolantFlowRate { get; }
        LitersPerMinute PowerTrainCoolantFlowRate { get; }
    }
}