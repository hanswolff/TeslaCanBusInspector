// ReSharper disable UnusedMember.Global

using TeslaCanBusInspector.ValueTypes;

namespace TeslaCanBusInspector.Models
{
    public class DcDcInfoMessage : IDcDcInfoMessage
    {
        public const ushort TypeId = 0x210;
        public ushort MessageTypeId => TypeId;

        public Amps DcDcCurrent { get; }
        public Volts DcDcVoltage { get; }
        public decimal DcDcCoolantInlet { get; }
        public Watts DcDcInputPower { get; }
        public Watts DcDcOutputPower { get; }

        internal DcDcInfoMessage()
        {
        }

        public DcDcInfoMessage(byte[] payload)
        {
            payload.RequireBytes(6);

            DcDcCurrent = new Amps(payload[4]);
            DcDcVoltage = new Volts(payload[5] / 10.0m);
            DcDcCoolantInlet = (payload[2] - 2 * (payload[2] & 0x80)) / 2m + 40m;
            DcDcInputPower = new Watts((ushort)(payload[3] << 4));
            DcDcOutputPower = new Watts(payload[4] * payload[5] / 10.0m);
        }
    }

    public interface IDcDcInfoMessage : ICanBusMessage
    {
        Amps DcDcCurrent { get; }
        Volts DcDcVoltage { get; }
        decimal DcDcCoolantInlet { get; }
        Watts DcDcInputPower { get; }
        Watts DcDcOutputPower { get; }
    }
}