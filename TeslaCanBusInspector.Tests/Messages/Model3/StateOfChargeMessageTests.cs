using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class StateOfChargeMessageTests
    {
        private readonly byte[] _examplePayload = { 0x7A, 0xE3, 0x4D, 0x78, 0xE0, 0x0A, 0x03, 0x00 };

        [Fact]
        public void StateOfChargeAvg()
        {
            // Act      
            var message = new StateOfChargeMessage(_examplePayload);

            // Assert
            message.StateOfChargeAvg.Should().Be(new Percent(89.7m));
        }

        [Fact]
        public void StateOfChargeMax()
        {
            // Act      
            var message = new StateOfChargeMessage(_examplePayload);

            // Assert
            message.StateOfChargeMax.Should().Be(new Percent(90m));
        }

        [Fact]
        public void StateOfChargeMin()
        {
            // Act      
            var message = new StateOfChargeMessage(_examplePayload);

            // Assert
            message.StateOfChargeMin.Should().Be(new Percent(88.8m));
        }

        [Fact]
        public void StateOfChargeUI()
        {
            // Act      
            var message = new StateOfChargeMessage(_examplePayload);

            // Assert
            message.StateOfChargeUI.Should().Be(new Percent(89m));
        }
    }
}