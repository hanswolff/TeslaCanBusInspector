﻿using FluentAssertions;
using TeslaCanBusInspector.Models;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class CountryCodeMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x44, 0x45, 0x70, 0, 0, 0, 0, 0 };

        [Fact]
        public void Germany()
        {
            // Act      
            var message = new CountryCodeMessage(_examplePayload);

            // Assert
            message.CountryCode.Should().Be("DE");
        }
    }
}