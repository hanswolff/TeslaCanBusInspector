using System;
using System.Text;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class VinMessage : IVinMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x508;
        public byte RequireBytes => 8;

        public byte VinPartIndex { get; }
        public string VinPartValue { get; }

        internal VinMessage()
        {
        }

        public VinMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            VinPartIndex = payload[0];

            var segment = new ArraySegment<byte>(payload, 1, payload.Length - 1);
            VinPartValue = Encoding.Default.GetString(segment).TrimEnd('\0', ' ');
        }
    }

    public interface IVinMessage : ICanBusMessage
    {
        byte VinPartIndex { get; }
        string VinPartValue { get; }
    }
}