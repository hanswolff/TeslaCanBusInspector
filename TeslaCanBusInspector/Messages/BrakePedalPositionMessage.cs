// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Messages
{
    public class BrakePedalPositionMessage : IBrakePedalPositionMessage
    {
        public const ushort TypeId = 0x168;
        public ushort MessageTypeId => TypeId;

        public byte BrakePedalPositionPercent { get; }

        internal BrakePedalPositionMessage()
        {
        }

        public BrakePedalPositionMessage(byte[] payload)
        {
            payload.RequireBytes(2);

            BrakePedalPositionPercent = (byte)(payload[0] + (payload[1] << 8) - 3239);
        }
    }

    public interface IBrakePedalPositionMessage : ICanBusMessage
    {
        byte BrakePedalPositionPercent { get; }
    }
}