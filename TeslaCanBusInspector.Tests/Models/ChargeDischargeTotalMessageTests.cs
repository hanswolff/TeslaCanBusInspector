﻿using FluentAssertions;
using TeslaCanBusInspector.Models;
using TeslaCanBusInspector.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Models
{
    public class ChargeDischargeTotalMessageTests
    {
        private readonly byte[] _examplePayload = new byte[] { 0xCD, 0x05, 0x59, 0x00, 0x76, 0xB1, 0x4F, 0x00 };

        [Fact]
        public void ChargeTotal()
        {
            // Act      
            var message = new ChargeDischargeTotalMessage(_examplePayload);

            // Assert
            message.ChargeTotal.Should().Be(new KiloWattHour(5834.189));
        }

        [Fact]
        public void DischargeTotal()
        {
            // Act      
            var message = new ChargeDischargeTotalMessage(_examplePayload);

            // Assert
            message.DischargeTotal.Should().Be(new KiloWattHour(5222.774));
        }
    }
}