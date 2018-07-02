﻿using FluentAssertions;
using TeslaCanBusInspector.Models;
using TeslaCanBusInspector.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class RearTorqueMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0x18, 0xB2, 0x10, 0x11, 0x0F, 0x7A, 0x3F, 0x08 };

        [Fact]
        public void RearTorque()
        {
            // Act      
            var message = new RearTorqueMessage(_examplePayload);

            // Assert
            message.RearTorque.Should().Be(new NewtonMeter(-33.50));
        }
    }
}