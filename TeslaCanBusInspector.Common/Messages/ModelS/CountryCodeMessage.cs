using System.Linq;

// ReSharper disable UnusedMember.Global
namespace TeslaCanBusInspector.Common.Messages.ModelS
{
    public class CountryCodeMessage : ICountryCodeMessage
    {
        public CarType CarType => CarType.ModelS | CarType.ModelX;

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