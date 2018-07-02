using FluentAssertions;
using TeslaCanBusInspector.Models;
using TeslaCanBusInspector.ValueTypes;
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
            message.DcDcCoolantInlet.Should().Be(25m);
        }

        [Fact]
        public void DcDcCurrent()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcCurrent.Should().Be(new Amps(28));
        }

        [Fact]
        public void DcDcInputPower()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcInputPower.Should().Be(new Watts(416));
        }
        
        [Fact]
        public void DcDcOutputPower()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcOutputPower.Should().Be(new Watts(378));
        }
        
        [Fact]
        public void DcDcVoltage()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcVoltage.Should().Be(new Volts(13.5));
        }
    }
}