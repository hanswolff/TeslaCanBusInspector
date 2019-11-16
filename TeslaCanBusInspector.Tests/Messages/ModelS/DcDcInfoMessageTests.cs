using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.ModelS;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.ModelS
{
    public class DcDcInfoMessageTests
    {
        private readonly byte[] _examplePayload = { 0x00, 0x00, 0xE2, 0x1A, 0x1C, 0x87, 0x00 };

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
            message.DcDcCurrent.Should().Be(new Ampere(28));
        }

        [Fact]
        public void DcDcInputPower()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcInputPower.Should().Be(new Watt(416));
        }
        
        [Fact]
        public void DcDcOutputPower()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcOutputPower.Should().Be(new Watt(378));
        }
        
        [Fact]
        public void DcDcVoltage()
        {
            // Act      
            var message = new DcDcInfoMessage(_examplePayload);

            // Assert
            message.DcDcVoltage.Should().Be(new Volt(13.5));
        }
    }
}