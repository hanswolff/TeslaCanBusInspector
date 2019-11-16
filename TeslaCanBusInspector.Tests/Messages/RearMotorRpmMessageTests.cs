using FluentAssertions;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages
{
    public class RearMotorRpmMessageTests
    {
        private readonly byte[] _examplePayload = { 0x05, 0x20, 0x04, 0x80, 0xB3, 0x00, 0x00, 0x63 };

        [Fact]
        public void RearRpm()
        {
            // Act      
            var message = new RearMotorRpmMessage(_examplePayload);

            // Assert
            message.RearRpm.Should().Be(new RevolutionsPerMinute(179));
        }
    }
}