using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class FastChargeLimitsMessageTests
    {
        private readonly byte[] _examplePayload = { 0x40, 0x80, 0x00, 0x01, 0x30, 0x11, 0x00, 0x80 };

        [Fact]
        public void FastChargeCurrentLimit()
        {
            // Act      
            var message = new FastChargeLimitsMessage(_examplePayload);

            // Assert
            message.FastChargeCurrentLimit.Should().Be(new Ampere(18.75m));
        }

        [Fact]
        public void FastChargeMaxVoltage()
        {
            // Act      
            var message = new FastChargeLimitsMessage(_examplePayload);

            // Assert
            message.FastChargeMaxVoltage.Should().Be(new Volt(322.26m));
        }

        [Fact]
        public void FastChargeMinVoltage()
        {
            // Act      
            var message = new FastChargeLimitsMessage(_examplePayload);

            // Assert
            message.FastChargeMinVoltage.Should().Be(new Volt(0m));
        }

        [Fact]
        public void FastChargePowerLimit()
        {
            // Act      
            var message = new FastChargeLimitsMessage(_examplePayload);

            // Assert
            message.FastChargePowerLimit.Should().Be(new KiloWatt(3.9846m));
        }
    }
}