using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class BatteryCapacityMessageTests
    {
        private readonly byte[] _examplePayload = {0xEE, 0x96, 0x52, 0x4A, 0x29, 0x02, 0x8A, 0x00};

        [Fact]
        public void EnergyBuffer()
        {
            // Act      
            var message = new BatteryCapacityMessage(_examplePayload);

            // Assert
            message.EnergyBuffer.Should().Be(new KiloWattHour(3.4m));
        }

        [Fact]
        public void ExpectedRemainingCapacity()
        {
            // Act      
            var message = new BatteryCapacityMessage(_examplePayload);

            // Assert
            message.ExpectedRemainingCapacity.Should().Be(new KiloWattHour(16.5m));
        }

        [Fact]
        public void FullBatteryCapacity()
        {
            // Act      
            var message = new BatteryCapacityMessage(_examplePayload);

            // Assert
            message.FullBatteryCapacity.Should().Be(new KiloWattHour(75m));
        }

        [Fact]
        public void IdealRemaining()
        {
            // Act      
            var message = new BatteryCapacityMessage(_examplePayload);

            // Assert
            message.IdealRemaining.Should().Be(new KiloWattHour(16.5m));
        }

        [Fact]
        public void RemainingBatteryCapacity()
        {
            // Act      
            var message = new BatteryCapacityMessage(_examplePayload);

            // Assert
            message.RemainingBatteryCapacity.Should().Be(new KiloWattHour(16.5m));
        }

        [Fact]
        public void ToCompleteCharge()
        {
            // Act      
            var message = new BatteryCapacityMessage(_examplePayload);

            // Assert
            message.ToCompleteCharge.Should().Be(new KiloWattHour(51.4m));
        }
    }
}