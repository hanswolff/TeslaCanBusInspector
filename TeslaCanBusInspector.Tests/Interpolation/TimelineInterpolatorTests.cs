using System;
using FluentAssertions;
using TeslaCanBusInspector.Common.Interpolation;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.Session;
using TeslaCanBusInspector.Tests.TestHelpers;
using Xunit;

namespace TeslaCanBusInspector.Tests.Interpolation
{
    public class TimelineInterpolatorTests
    {
        private readonly byte[] _batteryPowerPayload = { 0xA8, 0x87, 0xE2, 0xFF, 0xD3, 0x26, 0xFF, 0x0F };

        private readonly ITimelineInterpolator _timelineInterpolator = new TimelineInterpolator();

        [Fact]
        public void InterpolateTime()
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
            _timelineInterpolator.InterpolateTime(timeline);

            // Assert
            timeline[0].Timestamp.Should().BeNull();
            timeline[2].Timestamp.Should().Be(new DateTime(2019, 1, 1, 0, 0, 0, 250, DateTimeKind.Utc));
            timeline[3].Timestamp.Should().Be(new DateTime(2019, 1, 1, 0, 0, 0, 500, DateTimeKind.Utc));
            timeline[4].Timestamp.Should().Be(new DateTime(2019, 1, 1, 0, 0, 0, 750, DateTimeKind.Utc));
        }
    }
}
