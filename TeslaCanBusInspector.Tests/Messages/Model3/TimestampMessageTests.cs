using System;
using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class TimestampMessageTests
    {
        private readonly byte[] _examplePayload = { 0x13, 0x0B, 0x03, 0x05, 0x05, 0x15, 0x11, 0x6C };

        [Fact]
        public void Timestamp()
        {
            // Act      
            var message = new TimestampMessage(_examplePayload);

            // Assert
            message.Timestamp.Should().Be(new DateTime(2019, 11, 5, 5, 21, 3, DateTimeKind.Utc));
        }
    }
}