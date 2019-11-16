using System;
using TeslaCanBusInspector.Common.Messages.ModelS;
using TeslaCanBusInspector.Common.ValueTypes;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.Model3
{
    public class OdometerMessage : IOdometerMessage
    {
        public CarType CarType => CarType.Model3;

        public const ushort TypeId = 0x3B6;
        public ushort MessageTypeId => TypeId;

        public Kilometer Odometer { get; }

        internal OdometerMessage()
        {
        }

        public OdometerMessage(byte[] payload)
        {
            payload.RequireBytes(4);

            Odometer = new Kilometer(BitConverter.ToUInt32(payload) / 1000m);
        }
    }

    public interface IOdometerMessage : ICanBusMessage
    {
        Kilometer Odometer { get; }
    }
}