using FluentAssertions;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages
{
    public class RearMechPowerMessageTests
    {
        private readonly byte[] _examplePayload = { 0x85, 0x07, 0xFD, 0x07, 0x40, 0xA0, 0x4A, 0x48 };

        [Fact]
        public void RearMechPower()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearMechPower.Should().Be(new KiloWatt(-1.5));
        }

        [Fact]
        public void RearDissipation()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearDissipation.Should().Be(new KiloWatt(0.375));
        }

        [Fact]
        public void RearDriveMaxPower()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearDriveMaxPower.Should().Be(new KiloWatt(85));
        }

        [Fact]
        public void RearInverterVoltage()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearInverterVoltage.Should().Be(new Volt(13.3));
        }

        [Fact]
        public void RearRegenMaxPower()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearRegenMaxPower.Should().Be(new KiloWatt(88));
        }

        [Fact]
        public void RearInputPower()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearInputPower.Should().Be(new KiloWatt(-1.125));
        }

        [Fact]
        public void RearStatorCurrent()
        {
            // Act      
            var message = new RearMechPowerMessage(_examplePayload);

            // Assert
            message.RearStatorCurrent.Should().Be(new Ampere(64));
        }
    }
}