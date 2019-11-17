using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class FrontTorqueMessageTests
    {
        private readonly byte[] _examplePayload = { 0x13, 0x08, 0x00, 0x00, 0x00, 0x66, 0x1E, 0x00 };

        [Fact]
        public void FrontAxleRpm()
        {
            // Act      
            var message = new FrontTorqueMessage(_examplePayload);

            // Assert
            message.FrontAxleRpm.Should().Be(new RevolutionsPerMinute(778.2m));
        }

        [Fact]
        public void FrontTorque()
        {
            // Act      
            var message = new FrontTorqueMessage(_examplePayload);

            // Assert
            message.FrontTorque.Should().Be(new NewtonMeter(0));
        }

        [Fact]
        public void FrontTorqueRequest()
        {
            // Act      
            var message = new FrontTorqueMessage(_examplePayload);

            // Assert
            message.FrontTorqueRequest.Should().Be(new NewtonMeter(0));
        }
    }
}