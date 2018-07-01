using FluentAssertions;
using TeslaCanBusInspector.Models;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class DcDcInfoMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x00, 0x00, 0xE2, 0x1A, 0x1C, 0x87, 0x00 };

        [Fact]
        public void DcDcCoolantInlet()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcCoolantInlet.Should().Be(25M);
        }

        [Fact]
        public void DcDcCurrent()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcCurrent.Should().Be(28);
        }

        [Fact]
        public void DcDcInputPowerWatts()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcInputPowerWatts.Should().Be(416);
        }
        
        [Fact]
        public void DcDcOutputPowerWatts()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcOutputPowerWatts.Should().Be(378m);
        }
        
        [Fact]
        public void DcDcVoltage()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcVoltage.Should().Be(13.5m);
        }
    }
}