using FluentAssertions;
using TeslaCanBusInspector.Models;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class StateOfChargeMessageTests
    {
        [Fact]
        public void StateOfChargeMin()
        {
            // Act      
            var message = new StateOfChargeMessage(new byte[] { 0x1F, 0x2F, 0x1C, 0x00, 0x9F, 0xE9, 0x10, 0x00 } );

            // Assert
            message.StateOfChargeMin.Should().Be(79.9m);
        }

        [Fact]
        public void StateOfChargeDisplayed()
        {
            // Act      
            var message = new StateOfChargeMessage(new byte[] { 0x1F, 0x2F, 0x1C, 0x00, 0x9F, 0xE9, 0x10, 0x00 } );

            // Assert
            message.StateOfChargeDisplayed.Should().Be(87.8m);
        }
    }
}