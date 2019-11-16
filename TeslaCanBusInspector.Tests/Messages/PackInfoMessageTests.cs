using FluentAssertions;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages
{
    public class PackInfoMessageTests
    {
        private readonly byte[] _examplePayload = { 0xCE, 0xB6, 0x08, 0xE2, 0x8B, 0x00, 0xA0, 0x1C };

        [Fact]
        public void EnergyBuffer()
        {
            // Act      
            var message = new PackInfoMessage(_examplePayload);

            // Assert
            message.EnergyBuffer.Should().Be(new KiloWattHour(4));
        }

        [Fact]
        public void ExpectedRemaining()
        {
            // Act      
            var message = new PackInfoMessage(_examplePayload);

            // Assert
            message.ExpectedRemaining.Should().Be(new KiloWattHour(54.4));
        }

        [Fact]
        public void IdealRemaining()
        {
            // Act      
            var message = new PackInfoMessage(_examplePayload);

            // Assert
            message.IdealRemaining.Should().Be(new KiloWattHour(56.8));
        }

        [Fact]
        public void NominalFullPack()
        {
            // Act      
            var message = new PackInfoMessage(_examplePayload);

            // Assert
            message.NominalFullPack.Should().Be(new KiloWattHour(71.8));
        }

        [Fact]
        public void NominalRemaining()
        {
            // Act      
            var message = new PackInfoMessage(_examplePayload);

            // Assert
            message.NominalRemaining.Should().Be(new KiloWattHour(55.7));
        }

        [Fact]
        public void ToChargeComplete()
        {
            // Act      
            var message = new PackInfoMessage(_examplePayload);

            // Assert
            message.ToChargeComplete.Should().Be(new KiloWattHour(0));
        }
    }
}