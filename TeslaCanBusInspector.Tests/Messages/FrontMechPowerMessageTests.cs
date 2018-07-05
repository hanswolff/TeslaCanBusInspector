using FluentAssertions;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages
{
    public class FrontMechPowerMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x87, 0x07, 0xFD, 0x07, 0x43, 0x48, 0x09, 0x00 };

        [Fact]
        public void FrontMechPower()
        {
            // Act      
            var message = new FrontMechPowerMessage(_examplePayload);

            // Assert
            message.FrontMechPower.Should().Be(new KiloWatt(-1));
        }

        [Fact]
        public void FrontDissipation()
        {
            // Act      
            var message = new FrontMechPowerMessage(_examplePayload);

            // Assert
            message.FrontDissipation.Should().Be(new KiloWatt(0.375));
        }

        [Fact]
        public void FrontDriveMaxPower()
        {
            // Act      
            var message = new FrontMechPowerMessage(_examplePayload);

            // Assert
            message.FrontDriveMaxPower.Should().Be(new KiloWatt(9));
        }

        [Fact]
        public void FrontInputPower()
        {
            // Act      
            var message = new FrontMechPowerMessage(_examplePayload);

            // Assert
            message.FrontInputPower.Should().Be(new KiloWatt(-0.625));
        }

        [Fact]
        public void FrontStatorCurrent()
        {
            // Act      
            var message = new FrontMechPowerMessage(_examplePayload);

            // Assert
            message.FrontStatorCurrent.Should().Be(new Ampere(67));
        }
    }
}