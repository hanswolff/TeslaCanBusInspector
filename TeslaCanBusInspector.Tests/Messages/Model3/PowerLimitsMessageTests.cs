using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class PowerLimitsMessageTests
    {
        private readonly byte[] _examplePayload = { 0x34, 0x21, 0xB1, 0x95, 0x00, 0x00, 0xA1, 0x0F };

        [Fact]
        public void FastChargeCurrentLimit()
        {
            // Act      
            var message = new PowerLimitsMessage(_examplePayload);

            // Assert
            message.DischargePowerLimit.Should().Be(new KiloWatt(383.21m));
        }

        [Fact]
        public void FastChargeMaxVoltage()
        {
            // Act      
            var message = new PowerLimitsMessage(_examplePayload);

            // Assert
            message.RegenPowerLimit.Should().Be(new KiloWatt(85m));
        }
    }
}