using FluentAssertions;
using TeslaCanBusInspector.Models;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class BmsInfoMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x4E, 0x22, 0xE9, 0x84 };

        [Fact]
        public void BmsMaxCharge()
        {
            // Act      
            var message = new BmsInfoMessage(_examplePayload);

            // Assert
            message.BmsMaxCharge.Should().Be(87.82m);
        }
        
        [Fact]
        public void BmsMaxDischarge()
        {
            // Act      
            var message = new BmsInfoMessage(_examplePayload);

            // Assert
            message.BmsMaxDischarge.Should().Be(340.25m);
        }
    }
}