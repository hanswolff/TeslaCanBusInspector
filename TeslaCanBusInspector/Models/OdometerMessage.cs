// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Models
{
    public class OdometerMessage : IOdometerMessage
    {
        public const decimal MilesToKm = 1.609344m;

        public const ushort TypeId = 0x562;
        public ushort MessageTypeId => TypeId;

        public decimal OdometerValueMiles { get; }
        public decimal OdometerValueKm { get; }

        internal OdometerMessage()
        {
        }

        public OdometerMessage(byte[] payload)
        {
            payload.RequireBytes(4);

            OdometerValueMiles = (payload[0] + (payload[1] << 8) + (payload[2] << 16) + (payload[3] << 24)) / 1000.0m;
            OdometerValueKm = OdometerValueMiles * MilesToKm;
        }
    }

    public interface IOdometerMessage : ICanBusMessage
    {
        decimal OdometerValueMiles { get; }
        decimal OdometerValueKm { get; }
    }
}