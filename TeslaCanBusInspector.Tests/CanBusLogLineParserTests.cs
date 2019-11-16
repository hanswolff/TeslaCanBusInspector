using FluentAssertions;
using TeslaCanBusInspector.Common;
using Xunit;

namespace TeslaCanBusInspector.Tests
{
    public class CanBusLogLineParserTests
    {
        [Fact]
        public void TryParseLine_CanDumpFormat()
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

        [Fact]
        public void TryParseLine_CanDumpFormat_Twice()
        {
            // Arrange
            const string line = "(1530450358.333668) can0 106#04A00480BC0000EB";

            // Act
            var parsedLine1 = new CanBusLogLineParser().TryParseLine(line);
            var parsedLine2 = new CanBusLogLineParser().TryParseLine(line);

            // Assert
            parsedLine2.Should().BeEquivalentTo(parsedLine1);
        }

        [Fact]
        public void TryParseLine_ScanMyTeslaFormat()
        {
            // Arrange
            const string line = "10222330000A8FE2008";

            // Act
            var parsedLine = new CanBusLogLineParser().TryParseLine(line);

            // Assert
            parsedLine.UnixTimestamp.Should().Be(0);
            parsedLine.CanInterface.Should().BeNull();
            parsedLine.MessageTypeId.Should().Be(0x102);
            parsedLine.Payload.Should().BeEquivalentTo(new [] { 0x22, 0x33, 0x00, 0x00, 0xA8, 0xFE, 0x20, 0x08 });
        }

        [Fact]
        public void TryParseLine_AlternatingFormats()
        {
            // Arrange
            const string line1 = "BUFFER FULL";
            const string line2 = "10222330000A8FE2008";
            const string line3 = "(1530450358.333668) can0 106#04A00480BC0000EB";

            // Act
            var parsedLine1 = new CanBusLogLineParser().TryParseLine(line1);
            var parsedLine2 = new CanBusLogLineParser().TryParseLine(line2);
            var parsedLine3 = new CanBusLogLineParser().TryParseLine(line3);

            // Assert
            parsedLine1.Should().BeNull();
            parsedLine2.Should().NotBeNull();
            parsedLine3.Should().NotBeNull();
        }
    }
}
