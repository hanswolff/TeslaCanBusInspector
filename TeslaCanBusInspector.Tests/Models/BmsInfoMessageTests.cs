using FluentAssertions;
using TeslaCanBusInspector.Models;
using TeslaCanBusInspector.ValueTypes;
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
            message.BmsMaxCharge.Should().Be(new KiloWatts(87.82));
        }
        
        [Fact]
        public void BmsMaxDischarge()
        {
            // Act      
            var message = new BmsInfoMessage(_examplePayload);

            // Assert
            message.BmsMaxDischarge.Should().Be(new KiloWatts(340.25));
        }
    }
}