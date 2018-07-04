using FluentAssertions;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages
{
    public class FrontMechPowerMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        [Fact]
        public void FrontMechPower()
        {
            // Act      
            var message = new FrontMechPowerMessage(_examplePayload);

            // Assert
            message.FrontMechPower.Should().Be(new KiloWatt(0));
        }

        [Fact]
        public void FrontDissipation()
        {
            // Act      
            var message = new FrontMechPowerMessage(_examplePayload);

            // Assert
            message.FrontDissipation.Should().Be(new KiloWatt(0));
        }

        [Fact]
        public void FrontDriveMaxPower()
        {
            // Act      
            var message = new FrontMechPowerMessage(_examplePayload);

            // Assert
            message.FrontDriveMaxPower.Should().Be(new KiloWatt(0));
        }

        [Fact]
        public void FrontInputPower()
        {
            // Act      
            var message = new FrontMechPowerMessage(_examplePayload);

            // Assert
            message.FrontInputPower.Should().Be(new KiloWatt(0));
        }

        [Fact]
        public void FrontStatorCurrent()
        {
            // Act      
            var message = new FrontMechPowerMessage(_examplePayload);

            // Assert
            message.FrontStatorCurrent.Should().Be(new Ampere(0));
        }
    }
}