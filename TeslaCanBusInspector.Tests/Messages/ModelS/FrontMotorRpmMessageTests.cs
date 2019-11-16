using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.ModelS;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.ModelS
{
    public class FrontMotorRpmMessageTests
    {
        private readonly byte[] _examplePayload = { 0x05, 0xC0, 0x05, 0x80, 0xCE, 0x00, 0x2E };

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