using FluentAssertions;
using TeslaCanBusInspector.Models;
using TeslaCanBusInspector.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class FrontTorqueMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x00, 0x04, 0x00, 0x00, 0x08, 0xEE, 0xC0, 0x8F };

        [Fact]
        public void FrontTorque()
        {
            // Act      
            var message = new FrontTorqueMessage(_examplePayload);

            // Assert
            message.FrontTorque.Should().Be(new NewtonMeter(59.50));
        }
    }
}