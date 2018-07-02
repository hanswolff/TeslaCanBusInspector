using FluentAssertions;
using TeslaCanBusInspector.Models;
using TeslaCanBusInspector.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class OdometerMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x9B, 0x61, 0xAC, 0x00 };

        [Fact]
        public void OdometerValueMiles()
        {
            // Act      
            var message = new OdometerMessage(_examplePayload);

            // Assert
            message.OdometerValue.Should().Be(new Mile(11297.179));
        }

        [Fact]
        public void OdometerValueKm()
        {
            // Act      
            var message = new OdometerMessage(_examplePayload);

            // Assert
            ((Kilometer)message.OdometerValue).Should().Be(new Kilometer(18181.047240576m));
        }
    }
}