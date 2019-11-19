using System.Linq;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class CountryCodeMessage : ICountryCodeMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;
        public ushort MessageTypeId => 0x398;
        public byte RequireBytes => 2;

        public string CountryCode { get; }

        internal CountryCodeMessage()
        {
        }

        public CountryCodeMessage(byte[] payload)
        {
            payload.RequireBytes(RequireBytes);

            CountryCode = new string(new [] { payload[0], payload[1] }.Select(p => (char)p).ToArray());
        }
    }

    public interface ICountryCodeMessage : ICanBusMessage
    {
        string CountryCode { get; }
    }
}