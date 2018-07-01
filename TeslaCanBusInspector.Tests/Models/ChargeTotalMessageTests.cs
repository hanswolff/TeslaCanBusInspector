using FluentAssertions;
using TeslaCanBusInspector.Models;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class ChargeTotalMessageTests
    {
        [Fact]
        public void ChargeTotal()
        {
            // Act      
            var message = new ChargeTotalMessage(new byte[] { 0xCD, 0x05, 0x59, 0x00, 0x76, 0xB1, 0x4F, 0x00 } );

            // Assert
            message.ChargeTotalKwh.Should().Be(5834.189m);
        }
    }
}