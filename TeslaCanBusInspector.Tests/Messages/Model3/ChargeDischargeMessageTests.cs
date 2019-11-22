using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class ChargeDischargeMessageTests
    {
        private readonly byte[] _examplePayload = {0x08, 0x6C, 0x41, 0x00, 0xAA, 0xDC, 0x44, 0x00};

        [Fact]
        public void TotalCharge()
        {
            // Act      
            var message = new ChargeDischargeMessage(_examplePayload);

            // Assert
            message.TotalCharge.Should().Be(new KiloWattHour(4512.938m));
        }

        [Fact]
        public void TotalDischarge()
        {
            // Act      
            var message = new ChargeDischargeMessage(_examplePayload);

            // Assert
            message.TotalDischarge.Should().Be(new KiloWattHour(4287.496m));
        }
    }
}