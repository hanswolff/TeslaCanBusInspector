// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Models
{
    public class BmsInfoMessage : IBmsInfoMessage
    {
        public const ushort TypeId = 0x232;
        public ushort MessageTypeId => TypeId;

        public decimal BmsMaxCharge { get; }
        public decimal BmsMaxDischarge { get; }

        internal BmsInfoMessage()
        {
        }

        public BmsInfoMessage(byte[] payload)
        {
            payload.RequireBytes(4);

            BmsMaxCharge = (payload[0] + (payload[1] << 8)) / 100.0m;
            BmsMaxDischarge = (payload[2] + (payload[3] << 8)) / 100.0m;
        }
    }

    public interface IBmsInfoMessage : ICanBusMessage
    {
        decimal BmsMaxCharge { get; }
        decimal BmsMaxDischarge { get; }
    }
}