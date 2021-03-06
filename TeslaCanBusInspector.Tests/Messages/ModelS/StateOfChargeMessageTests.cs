﻿using FluentAssertions;
using TeslaCanBusInspector.Common.Messages.ModelS;
using TeslaCanBusInspector.Common.ValueTypes;
using Xunit;

namespace TeslaCanBusInspector.Tests.Messages.ModelS
{
    public class StateOfChargeMessageTests
    {
        private readonly byte[] _examplePayloadAc = { 0x1F, 0x2F, 0x1C, 0x00, 0x9F, 0xE9, 0x10, 0x00 };
        private readonly byte[] _examplePayloadDc = { 0x1B, 0x23, 0x0C, 0x00, 0x0E, 0xE8, 0x3A, 0x00 };

        [Fact]
        public void ChargeTotalTypeAc()
        {
            // Act      
            var message = new StateOfChargeMessage(_examplePayloadAc);

            // Assert
            message.ChargeTotalType.Should().Be(ChargeTotalType.AC);
        }

        [Fact]
        public void ChargeTotalTypeDc()
        {
            // Act      
            var message = new StateOfChargeMessage(_examplePayloadDc);

            // Assert
            message.ChargeTotalType.Should().Be(ChargeTotalType.DC);
        }

        [Fact]
        public void ChargeTotalAc()
        {
            // Act      
            var message = new StateOfChargeMessage(_examplePayloadAc);

            // Assert
            message.ChargeTotal.Should().Be(new KiloWattHour(1108.383));
        }
        
        [Fact]
        public void ChargeTotalDc()
        {
            // Act      
            var message = new StateOfChargeMessage(_examplePayloadDc);

            // Assert
            message.ChargeTotal.Should().Be(new KiloWattHour(3860.494));
        }

        [Fact]
        public void StateOfChargeMin()
        {
            // Act      
            var message = new StateOfChargeMessage(_examplePayloadAc);

            // Assert
            message.StateOfChargeMin.Should().Be(new Percent(79.9));
        }

        [Fact]
        public void StateOfChargeDisplayed()
        {
            // Act      
            var message = new StateOfChargeMessage(_examplePayloadAc );

            // Assert
            message.StateOfChargeDisplayed.Should().Be(new Percent(87.8));
        }
    }
}