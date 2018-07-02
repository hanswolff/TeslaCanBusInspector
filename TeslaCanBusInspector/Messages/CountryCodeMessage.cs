using System.Linq;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Messages
{
    public class CountryCodeMessage : ICountryCodeMessage
    {
        public const ushort TypeId = 0x398;
        public ushort MessageTypeId => TypeId;

        public string CountryCode { get; }

        internal CountryCodeMessage()
        {
        }

        public CountryCodeMessage(byte[] payload)
        {
            payload.RequireBytes(2);

            CountryCode = new string(new [] { payload[0], payload[1] }.Select(p => (char)p).ToArray());
        }
    }

    public interface ICountryCodeMessage : ICanBusMessage
    {
        string CountryCode { get; }
    }
}