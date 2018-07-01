using FluentAssertions;
using TeslaCanBusInspector.Models;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class SteeringAngleMessageTests
    {
        [Fact]
        public void SteeringAngleDegrees()
        {
            // Act      
            var message = new SteeringAngleMessage(new byte[] { 0x20, 0x6E, 0x20, 0x00, 0x04, 0xFF, 0x20, 0x9F } );

            // Assert
            message.SteeringAngleDegrees.Should().Be(10.2m);
        }
    }
}