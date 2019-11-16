using FluentAssertions;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages
{
    public class TemperatureMessageTests
    {
        private readonly byte[] _examplePayload = { 0x7E, 0x7D, 0x8E, 0x14, 0x83, 0x00, 0x00, 0x00 };

        [Fact]
        public void AirConditioningTemperature()
        {
            // Act      
            var message = new TemperatureMessage(_examplePayload);

            // Assert
            message.AirConditioningTemperature.Should().Be(new Celsius(25.5));
        }

        [Fact]
        public void InsideTemperature()
        {
            // Act      
            var message = new TemperatureMessage(_examplePayload);

            // Assert
            message.InsideTemperature.Should().Be(new Celsius(31));
        }

        [Fact]
        public void OutsideTemperature()
        {
            // Act      
            var message = new TemperatureMessage(_examplePayload);

            // Assert
            message.OutsideTemperature.Should().Be(new Celsius(23.0));
        }

        [Fact]
        public void OutsideTemperatureFiltered()
        {
            // Act      
            var message = new TemperatureMessage(_examplePayload);

            // Assert
            message.OutsideTemperatureFiltered.Should().Be(new Celsius(22.5));
        }
    }
}