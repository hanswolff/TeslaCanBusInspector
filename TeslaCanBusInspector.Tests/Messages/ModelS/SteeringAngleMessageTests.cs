using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.ModelS;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.ModelS
{
    public class SteeringAngleMessageTests
    {
        private readonly byte[] _examplePayload = { 0x20, 0x6E, 0x20, 0x00, 0x04, 0xFF, 0x20, 0x9F };

        [Fact]
        public void SteeringAngleDegrees()
        {
            // Act      
            var message = new SteeringAngleMessage(_examplePayload);

            // Assert
            message.SteeringAngleDegrees.Should().Be(10.2m);
        }
    }
}