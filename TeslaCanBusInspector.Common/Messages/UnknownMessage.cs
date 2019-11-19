using System;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages
{
    public class UnknownMessage : ICanBusMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX | CarType.Model3;

        public const ushort TypeId = 0x0;
        public ushort MessageTypeId => TypeId;
        public byte RequireBytes => 0;

        public byte[] Payload { get; }

        internal UnknownMessage()
        {
        }

        public UnknownMessage(byte[] payload)
        {
            Payload = payload ?? throw new ArgumentNullException(nameof(payload));
        }
    }
}