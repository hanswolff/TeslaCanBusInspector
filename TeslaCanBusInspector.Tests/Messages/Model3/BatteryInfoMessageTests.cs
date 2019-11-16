using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class BatteryInfoMessageTests
    {
        private readonly byte[] _examplePayload = { 0xA8, 0x87, 0xE2, 0xFF, 0xD3, 0x26, 0xFF, 0x0F };

        [Fact]
        public void BatteryCurrentRaw()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BatteryCurrentRaw.Should().Be(new Ampere(3.05m));
        }

        [Fact]
        public void BatteryCurrentSmooth()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BatteryCurrentSmooth.Should().Be(new Ampere(0.3m));
        }

        [Fact]
        public void BatteryVoltage()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BatteryVoltage.Should().Be(new Volt(347.28m));
        }

        [Fact]
        public void ChargeTimeRemaining()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.ChargeTimeRemaining.TotalMinutes.Should().Be(1920.0);
        }
    }
}