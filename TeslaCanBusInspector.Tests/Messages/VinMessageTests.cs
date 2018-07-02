using FluentAssertions;
using TeslaCanBusInspector.Messages;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages
{
    public class VinMessageTests
    {
        [Fact]
        public void Vin1()
        {
            // Act      
            var message = new VinMessage(new byte[] { 0x00, 0x35, 0x59, 0x4A, 0x53, 0x41, 0x37, 0x45 });

            // Assert
            message.VinPartIndex.Should().Be(0);
            message.VinPartValue.Should().Be("5YJSA7E");
        }

        [Fact]
        public void Vin2()
        {
            // Act      
            var message = new VinMessage(new byte[] { 0x01, 0x32, 0x38, 0x48, 0x46, 0x31, 0x32, 0x33 });

            // Assert
            message.VinPartIndex.Should().Be(1);
            message.VinPartValue.Should().Be("28HF123");
        }

        [Fact]
        public void Vin3()
        {
            // Act      
            var message = new VinMessage(new byte[] { 0x02, 0x34, 0x35, 0x36, 0, 0, 0, 0 });

            // Assert
            message.VinPartIndex.Should().Be(2);
            message.VinPartValue.Should().Be("456");
        }
    }
}