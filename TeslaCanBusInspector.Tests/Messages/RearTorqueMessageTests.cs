using FluentAssertions;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages
{
    public class RearTorqueMessageTests
    {
        private readonly byte[] _examplePayload = { 0x18, 0xB2, 0x10, 0x11, 0x0F, 0x7A, 0x3F, 0x08 };

        [Fact]
        public void RearTorque()
        {
            // Act      
            var message = new RearTorqueMessage(_examplePayload);

            // Assert
            message.RearTorque.Should().Be(new NewtonMeter(-33.50));
        }

        [Fact]
        public void WattPedal()
        {
            // Act      
            var message = new RearTorqueMessage(_examplePayload);

            // Assert
            message.WattPedal.Should().Be(new Percent(6.80));
        }
    }
}