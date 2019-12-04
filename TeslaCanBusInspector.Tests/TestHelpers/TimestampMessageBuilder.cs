using System;
using TeslaCanBusInspector.Common.Messages.Model3;

namespace TeslaCanBusInspector.Tests.TestHelpers
{
    public class TimestampMessageBuilder
    {
        private byte[] _payload = new byte[0];

        public TimestampMessageBuilder(DateTime dateTime)
        {
            WithTimestamp(dateTime);
        }

        public TimestampMessageBuilder WithTimestamp(DateTime dateTime)
        {
            _payload = new byte[]
            {
                (byte) (dateTime.Year - 2000), (byte) dateTime.Month, (byte) dateTime.Second, (byte) dateTime.Hour,
                (byte) dateTime.Day, (byte) dateTime.Minute, 0, 0
            };
            return this;
        }

        public TimestampMessage Build()
        {
            return new TimestampMessage(_payload);
        }
    }
}
