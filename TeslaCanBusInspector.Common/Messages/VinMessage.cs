using System;
using System.Text;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages
{
    public class VinMessage : IVinMessage
    {
        public const ushort TypeId = 0x508;
        public ushort MessageTypeId => TypeId;

        public byte VinPartIndex { get; }

        public string VinPartValue { get; }

        internal VinMessage()
        {
        }

        public VinMessage(byte[] payload)
        {
            payload.RequireBytes(8);

            VinPartIndex = payload[0];
            VinPartValue = Encoding.Default.GetString(new ArraySegment<byte>(payload, 1, payload.Length - 1)).TrimEnd('\0', ' ');
        }
    }

    public interface IVinMessage : ICanBusMessage
    {
        byte VinPartIndex { get; }

        string VinPartValue { get; }
    }
}