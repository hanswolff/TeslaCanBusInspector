using FluentAssertions;
using TeslaCanBusInspector.Models;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class BmsInfoMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x42, 0x22, 0xD1, 0x84 };

        [Fact]
        public void BatteryCurrent()
        {
            // Act      
            var message = new BmsInfoMessage(_examplePayload);

            // Assert
            message.BmsMaxCharge.Should().Be(87.7m);
        }
        
        [Fact]
        public void BatteryVoltage()
        {
            // Act      
            var message = new BmsInfoMessage(_examplePayload);

            // Assert
            message.BmsMaxDischarge.Should().Be(336.64m);
        }
    }
}