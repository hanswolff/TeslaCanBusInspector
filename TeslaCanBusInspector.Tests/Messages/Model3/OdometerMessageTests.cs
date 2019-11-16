using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class OdometerMessageTests
    {
        private readonly byte[] _examplePayload = { 0x35, 0x6D, 0x1E, 0x01 };

        [Fact]
        public void Odometer()
        {
            // Act      
            var message = new OdometerMessage(_examplePayload);

            // Assert
            message.Odometer.Should().Be(new Kilometer(18771.253m));
        }
    }
}