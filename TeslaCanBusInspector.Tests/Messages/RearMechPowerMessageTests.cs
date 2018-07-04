using FluentAssertions;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages
{
    public class RearMechPowerMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        [Fact]
        public void RearMechPower()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearMechPower.Should().Be(new KiloWatt(0));
        }

        [Fact]
        public void RearDissipation()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearDissipation.Should().Be(new KiloWatt(0));
        }

        [Fact]
        public void RearDriveMaxPower()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearDriveMaxPower.Should().Be(new KiloWatt(0));
        }

        [Fact]
        public void RearInverterVoltage()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearInverterVoltage.Should().Be(new Volt(0));
        }

        [Fact]
        public void RearRegenMaxPower()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearRegenMaxPower.Should().Be(new KiloWatt(0));
        }

        [Fact]
        public void RearInputPower()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearInputPower.Should().Be(new KiloWatt(0));
        }

        [Fact]
        public void RearStatorCurrent()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearStatorCurrent.Should().Be(new Ampere(0));
        }
    }
}