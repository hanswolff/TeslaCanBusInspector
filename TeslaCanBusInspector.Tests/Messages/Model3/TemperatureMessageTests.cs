using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class TemperatureMessageTests
    {
        private readonly byte[] _examplePayload = { 0x24, 0x1A, 0xA9, 0x5A, 0x02, 0x5A, 0x00, 0x00 };

        [Fact]
        public void AmbientTempFiltered()
        {
            // Act      
            var message = new TemperatureMessage(_examplePayload);

            // Assert
            message.AmbientTempFiltered.Should().Be(new Celsius(5m));
        }

        [Fact]
        public void AmbientTempRaw()
        {
            // Act      
            var message = new TemperatureMessage(_examplePayload);

            // Assert
            message.AmbientTempRaw.Should().Be(new Celsius(5m));
        }

        [Fact]
        public void CoolantTempBatteryInlet()
        {
            // Act      
            var message = new TemperatureMessage(_examplePayload);

            // Assert
            message.CoolantTempBatteryInlet.Should().Be(new Celsius(28.5m));
        }

        [Fact]
        public void CoolantTempPowerTrainInlet()
        {
            // Act      
            var message = new TemperatureMessage(_examplePayload);

            // Assert
            message.CoolantTempPowerTrainInlet.Should().Be(new Celsius(32.75m));
        }
    }
}