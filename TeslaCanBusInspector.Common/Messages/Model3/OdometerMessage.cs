using System;
using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class OdometerMessage : IOdometerMessage
    {
        public CarType CarType => CarType.Model3;
        public ushort MessageTypeId => 0x3B6;
        public byte RequireBytes => 4;

        public Kilometer Odometer { get; }

        internal OdometerMessage()
        {
        }

        public OdometerMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            Odometer = new Kilometer(BitConverter.ToUInt32(payload) / 1000m);
        }
    }

    public interface IOdometerMessage : ICanBusMessage
    {
        Kilometer Odometer { get; }
    }
}