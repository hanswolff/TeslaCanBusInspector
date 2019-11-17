using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class RearTorqueMessageTests
    {
        private readonly byte[] _examplePayload = { 0x19, 0xAE, 0x01, 0xD0, 0x00, 0x73, 0x1E, 0x00 };

        [Fact]
        public void RearAxleRpm()
        {
            // Act      
            var message = new RearTorqueMessage(_examplePayload);

            // Assert
            message.RearAxleRpm.Should().Be(new RevolutionsPerMinute(779.5m));
        }

        [Fact]
        public void RearTorque()
        {
            // Act      
            var message = new RearTorqueMessage(_examplePayload);

            // Assert
            message.RearTorque.Should().Be(new NewtonMeter(416m));
        }

        [Fact]
        public void RearTorqueRequest()
        {
            // Act      
            var message = new RearTorqueMessage(_examplePayload);

            // Assert
            message.RearTorqueRequest.Should().Be(new NewtonMeter(416m));
        }
    }
}