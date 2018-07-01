using FluentAssertions;
using TeslaCanBusInspector.Models;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class OdometerMessageTests
    {
        [Fact]
        public void OdometerValueMiles()
        {
            // Act      
            var message = new OdometerMessage(new byte[] { 0x9B, 0x61, 0xAC, 0x00 } );

            // Assert
            message.OdometerValueMiles.Should().Be(11297.179M);
        }

        [Fact]
        public void OdometerValueKm()
        {
            // Act      
            var message = new OdometerMessage(new byte[] { 0x9B, 0x61, 0xAC, 0x00 } );

            // Assert
            message.OdometerValueKm.Should().Be(18181.047240576M);
        }
    }
}