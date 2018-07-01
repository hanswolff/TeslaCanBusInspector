// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Models
{
    public class DcDcInfoMessage : IDcDcInfoMessage
    {
        public const ushort TypeId = 0x210;
        public ushort MessageTypeId => TypeId;

        public byte DcDcCurrent { get; }
        public decimal DcDcVoltage { get; }
        public decimal DcDcCoolantInlet { get; }
        public ushort DcDcInputPowerWatts { get; }
        public decimal DcDcOutputPowerWatts { get; }

        internal DcDcInfoMessage()
        {
        }

        public DcDcInfoMessage(byte[] payload)
        {
            payload.RequireBytes(6);

            DcDcCurrent = payload[4];
            DcDcVoltage = payload[5] / 10.0m;
            DcDcCoolantInlet = (payload[2] - 2 * (payload[2] & 0x80)) / 2m + 40m;
            DcDcInputPowerWatts = (ushort)(payload[3] << 4);
            DcDcOutputPowerWatts = payload[4] * payload[5] / 10.0m;
        }
    }

    public interface IDcDcInfoMessage : ICanBusMessage
    {
        byte DcDcCurrent { get; }
        decimal DcDcVoltage { get; }
        decimal DcDcCoolantInlet { get; }
        ushort DcDcInputPowerWatts { get; }
        decimal DcDcOutputPowerWatts { get; }
    }
}