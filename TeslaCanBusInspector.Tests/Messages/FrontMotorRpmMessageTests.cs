using FluentAssertions;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages
{
    public class FrontMotorRpmMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x05, 0xC0, 0x05, 0x80, 0xCE, 0x00, 0x2E };

        [Fact]
        public void FrontRpm()
        {
            // Act      
            var message = new FrontMotorRpmMessage(_examplePayload);

            // Assert
            message.FrontRpm.Should().Be(new RevolutionsPerMinute(206));
        }
    }
}