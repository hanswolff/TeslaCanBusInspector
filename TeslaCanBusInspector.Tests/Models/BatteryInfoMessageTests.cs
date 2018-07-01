using FluentAssertions;
using TeslaCanBusInspector.Models;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class BatteryInfoMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x80, 0x83, 0x0F, 0xA7, 0x20, 0x4E, 0xFF, 0x03 };

        [Fact]
        public void BatteryCurrent()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BatteryCurrent.Should().Be(0.1M);
        }
        
        [Fact]
        public void BatteryVoltage()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BatteryVoltage.Should().Be(336.64m);
        }
                
        [Fact]
        public void BatteryPowerWatts()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BatteryPowerWatts.Should().Be(33.664m);
        }
                        
        [Fact]
        public void NegativeTerminal()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.NegativeTerminal.Should().Be(92.3M);
        }
    }
}