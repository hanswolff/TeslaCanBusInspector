// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class BrakePedalPositionMessage : IBrakePedalPositionMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x168;
        public byte RequireBytes => 2;

        public byte BrakePedalPositionPercent { get; }

        internal BrakePedalPositionMessage()
        {
        }

        public BrakePedalPositionMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            BrakePedalPositionPercent = (byte)(payload[0] + (payload[1] << 8) - 3239);
        }
    }

    public interface IBrakePedalPositionMessage : ICanBusMessage
    {
        byte BrakePedalPositionPercent { get; }
    }
}