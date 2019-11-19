using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.Model3
{
    public class SpeedMessageTests
    {
        private readonly byte[] _examplePayload = { 0x13, 0xF8, 0x34, 0x1C, 0x72, 0x00, 0x00, 0x00 };

        [Fact]
        public void SignedSpeed()
        {
            // Act      
            var message = new SpeedMessage(_examplePayload);

            // Assert
            message.SignedSpeed.Should().Be(new KilometerPerHour(27.76m));
        }

        [Fact]
        public void UISpeed()
        {
            // Act      
            var message = new SpeedMessage(_examplePayload);

            // Assert
            message.UISpeed.Should().Be(new KilometerPerHour(28.0m));
        }
    }
}