using FluentAssertions;
using TeslaCanBusInspector.Models;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class ChargeDischargeTotalMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0xCD, 0x05, 0x59, 0x00, 0x76, 0xB1, 0x4F, 0x00 };

        [Fact]
        public void ChargeTotal()
        {
            // Act      
            var message = new ChargeDischargeTotalMessage(_examplePayload);

            // Assert
            message.ChargeTotalKwh.Should().Be(5834.189m);
        }

        [Fact]
        public void DischargeTotal()
        {
            // Act      
            var message = new ChargeDischargeTotalMessage(_examplePayload);

            // Assert
            message.DischargeTotalKwh.Should().Be(5222.774m);
        }
    }
}