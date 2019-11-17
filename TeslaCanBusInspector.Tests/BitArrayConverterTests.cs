using System;
using FluentAssertions;
using TeslaCanBusInspector.Common;
using Xunit;

namespace TeslaCanBusInspector.Tests
{
    public class BitArrayConverterTests
    {
        [Fact]
        public void ToUInt16_BitConverter()
        {
            // Arrange
            var array = BitConverter.GetBytes(12345);

            // Act
            var result = BitArrayConverter.ToUInt16(array, 0, 16);

            // Assert
            result.Should().Be(12345);
        }

        [Fact]
        public void ToUInt32_BitConverter()
        {
            // Arrange
            var array = BitConverter.GetBytes(1234567890);

            // Act
            var result = BitArrayConverter.ToUInt32(array, 0, 32);

            // Assert
            result.Should().Be(1234567890);
        }
    }
}
