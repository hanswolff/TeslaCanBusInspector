using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class BatteryInfoMessageTests
    {
        private readonly byte[] _examplePayload = { 0x98, 0x04, 0xA6, 0x0D, 0x01, 0x00, 0x2E, 0x8D };

        [Fact]
        public void BmsChargePowerAvailable()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BmsChargePowerAvailable.Should().Be(new KiloWatt(0m));
        }

        [Fact]
        public void BmsChargeStatus()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BmsChargeStatus.Should().Be(1);
        }

        [Fact]
        public void BmsNumberOfContactors()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BmsNumberOfContactors.Should().Be(4);
        }

        [Fact]
        public void BmsState()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.BmsState.Should().Be(0);
        }

        [Fact]
        public void IsolationResistance()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.IsolationResistance.Should().Be(new KiloOhm(27904m));
        }

        [Fact]
        public void MinBatteryTemperature()
        {
            // Act      
            var message = new BatteryInfoMessage(_examplePayload);

            // Assert
            message.MinBatteryTemperature.Should().Be(new Celsius(30.5m));
        }
    }
}