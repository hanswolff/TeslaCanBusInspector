using FluentAssertions;
using TeslaCanBusInspector.Common;
using Xunit;

namespace TeslaCanBusInspector.Tests
{
    public class CanBusLogLineParserTests
    {
        [Fact]
        public void TryParseLine()
        {
            // Arrange
            const string line = "(1530450358.333668) can0 106#04A00480BC0000EB";

            // Act
            var parsedLine = new CanBusLogLineParser().TryParseLine(line);

            // Assert
            parsedLine.UnixTimestamp.Should().Be(1530450358.333668m);
            parsedLine.CanInterface.Should().Be("can0");
            parsedLine.MessageTypeId.Should().Be(0x106);
            parsedLine.Payload.Should().BeEquivalentTo(new [] { 0x04, 0xA0, 0x04, 0x80, 0xBC, 0x00, 0x00, 0xEB });
        }
    }
}
