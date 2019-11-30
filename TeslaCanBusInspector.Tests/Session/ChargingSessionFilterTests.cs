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
        private readonly byte[] _batteryPowerPayload = { 0xA8, 0x87, 0xE2, 0xFF, 0xD3, 0x26, 0xFF, 0x0F };
        private readonly byte[] _chargingStarted = { 0x7E, 0x9A, 0x3D, 0x01, 0x99, 0x29, 0x25, 0x00 };
        private readonly byte[] _chargingStopped = { 0x98, 0x04, 0xA6, 0x0D, 0x01, 0x00, 0x2E, 0x8D };

        [Fact]
        public void GetCharginSessions_NoChargingSession()
        {
            // Arrange
            var timeline = new MessageTimeline(new ICanBusMessage[]
            {
                new BatteryPowerMessage(_batteryPowerPayload),
                new TimestampMessageBuilder(new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Build(),
                new BatteryPowerMessage(_batteryPowerPayload),
                new BatteryPowerMessage(_batteryPowerPayload),
                new BatteryPowerMessage(_batteryPowerPayload),
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
                new BatteryPowerMessage(_batteryPowerPayload),
                new TimestampMessageBuilder(new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Build(),
                new BatteryInfoMessage(_chargingStarted), 
                new BatteryPowerMessage(_batteryPowerPayload),
                new BatteryPowerMessage(_batteryPowerPayload),
                new BatteryPowerMessage(_batteryPowerPayload),
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
                typeof(BatteryPowerMessage),
                typeof(BatteryPowerMessage));
        }
    }
}
