using FluentAssertions;
using TeslaCanBusInspector.Models;
using TeslaCanBusInspector.ValueTypes;
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
            message.BatteryCurrent.Should().Be(new Amps(0.1));
        }
        
        [Fact]
        public void BatteryVoltage()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BatteryVoltage.Should().Be(new Volts(336.64));
        }
                
        [Fact]
        public void BatteryPower()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BatteryPower.Should().Be(new Watts(33.664));
        }
                        
        [Fact]
        public void NegativeTerminal()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.NegativeTerminal.Should().Be(92.3m);
        }
    }
}