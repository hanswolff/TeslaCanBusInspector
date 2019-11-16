using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class BatteryInfoModel3MessageTests
    {
        private readonly byte[] _examplePayload = { 0xB4, 0x87, 0xF2, 0xFF, 0xF4, 0x26, 0xFF, 0x0F };

        [Fact]
        public void BatteryVoltage()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BatteryVoltage.Should().Be(new Volt(347.4m));
        }

        [Fact]
        public void BatteryCurrentRaw()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BatteryCurrentRaw.Should().Be(new Ampere(498.6m));
        }

        [Fact]
        public void BatteryCurrentSmooth()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BatteryCurrentSmooth.Should().Be(new Ampere(327.54m));
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