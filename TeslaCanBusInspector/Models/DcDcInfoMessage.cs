// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.ValueTypes;

namespace TeslaCanBusInspector.Models
{
    public class DcDcInfoMessage : IDcDcInfoMessage
    {
        public const ushort TypeId = 0x210;
        public ushort MessageTypeId => TypeId;

        public Ampere DcDcCurrent { get; }
        public Volt DcDcVoltage { get; }
        public decimal DcDcCoolantInlet { get; }
        public Watt DcDcInputPower { get; }
        public Watt DcDcOutputPower { get; }

        internal DcDcInfoMessage()
        {
        }

        public DcDcInfoMessage(byte[] payload)
        {
            payload.RequireBytes(6);

            DcDcCurrent = new Ampere(payload[4]);
            DcDcVoltage = new Volt(payload[5] / 10.0m);
            DcDcCoolantInlet = (payload[2] - 2 * (payload[2] & 0x80)) / 2m + 40m;
            DcDcInputPower = new Watt((ushort)(payload[3] << 4));
            DcDcOutputPower = new Watt(payload[4] * payload[5] / 10.0m);
        }
    }

    public interface IDcDcInfoMessage : ICanBusMessage
    {
        Ampere DcDcCurrent { get; }
        Volt DcDcVoltage { get; }
        decimal DcDcCoolantInlet { get; }
        Watt DcDcInputPower { get; }
        Watt DcDcOutputPower { get; }
    }
}