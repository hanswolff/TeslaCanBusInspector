using System;
using System.Linq;

namespace TeslaCanBusInspector.Models
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
            if (payload.Length < 2)
            {
                throw new ArgumentException($"{nameof(payload)} must have at least 2 bytes", nameof(payload));
            }

            CountryCode = new string(new [] { payload[0], payload[1] }.Select(p => (char)p).ToArray());
        }
    }

    public interface ICountryCodeMessage : ICanBusMessage
    {
        string CountryCode { get; }
    }
}