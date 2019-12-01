using System;
using System.Linq;
using FluentAssertions;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.Session;
using TeslaCanBusInspector.Tests.TestHelpers;
using Xunit;

namespace TeslaCanBusInspector.Tests.Session
{
    public class ChargingSessionFilterTests
    {
        private readonly byte[] _batteryPower = { 0xA8, 0x87, 0xE2, 0xFF, 0xD3, 0x26, 0xFF, 0x0F };
        private readonly byte[] _chargingStarted = { 0x7E, 0x9A, 0x3D, 0x01, 0x99, 0x29, 0x25, 0x00 };
        private readonly byte[] _chargingStopped = { 0x98, 0x04, 0xA6, 0x0D, 0x01, 0x00, 0x2E, 0x8D };
        private readonly byte[] _stateOfCharge = { 0x7A, 0xE3, 0x4D, 0x78, 0xE0, 0x0A, 0x03, 0x00 };

        [Fact]
        public void GetCharginSessions_NoChargingSession()
        {
            // Arrange
            var timeline = new MessageTimeline(new ICanBusMessage[]
            {
                new BatteryPowerMessage(_batteryPower),
                new TimestampMessageBuilder(new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Build(),
                new BatteryPowerMessage(_batteryPower),
                new StateOfChargeMessage(_stateOfCharge), 
                new BatteryPowerMessage(_batteryPower),
                new TimestampMessageBuilder(new DateTime(2019, 1, 1, 0, 0, 1, DateTimeKind.Utc)).Build(),
            });

            // Act
            var sessions = new ChargingSessionFilter().GetChargingSessions(timeline);

            // Assert
            sessions.Should().BeEmpty();
        }

        [Fact]
        public void GetCharginSessions_OneChargingSession()
        {
            // Arrange
            var timeline = new MessageTimeline(new ICanBusMessage[]
            {
                new BatteryPowerMessage(_batteryPower),
                new TimestampMessageBuilder(new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Build(),
                new BatteryInfoMessage(_chargingStarted), 
                new BatteryPowerMessage(_batteryPower),
                new StateOfChargeMessage(_stateOfCharge), 
                new BatteryPowerMessage(_batteryPower),
                new BatteryInfoMessage(_chargingStopped), 
                new TimestampMessageBuilder(new DateTime(2019, 1, 1, 0, 0, 1, DateTimeKind.Utc)).Build(),
            });

            // Act
            var sessions = new ChargingSessionFilter().GetChargingSessions(timeline).ToList();

            // Assert
            sessions.Count.Should().Be(1);
            sessions[0].Select(m => m.Value.GetType()).Should().BeEquivalentTo(
                typeof(BatteryInfoMessage),
                typeof(BatteryPowerMessage),
                typeof(StateOfChargeMessage),
                typeof(BatteryPowerMessage));
        }
    }
}
