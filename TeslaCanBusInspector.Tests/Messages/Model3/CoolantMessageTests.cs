using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class CoolantMessageTests
    {
        private readonly byte[] _examplePayload = { 0x35, 0x66, 0x18, 0x0F, 0x1E, 0x5B, 0x00 };

        [Fact]
        public void BatteryCoolantFlowRate()
        {
            // Act      
            var message = new CoolantMessage(_examplePayload);

            // Assert
            message.BatteryCoolantFlowRate.Should().Be(new LitersPerMinute(5.3m));
        }

        [Fact]
        public void PowerTrainCoolantFlowRate()
        {
            // Act      
            var message = new CoolantMessage(_examplePayload);

            // Assert
            message.PowerTrainCoolantFlowRate.Should().Be(new LitersPerMinute(6.0m));
        }
    }
}