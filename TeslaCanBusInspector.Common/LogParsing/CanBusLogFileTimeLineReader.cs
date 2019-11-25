using System;
using System.IO;
using System.Threading.Tasks;
using TeslaCanBusInspector.Common.Interpolation;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.Session;

namespace TeslaCanBusInspector.Common.LogParsing
{
    public class CanBusLogFileTimelineReader : ICanBusLogFileTimelineReader
    {
        private readonly ICanBusLogLineParser _canBusLogLineParser;
        private readonly ICanBusMessageFactory _canBusMessageFactory;
        private readonly ITimelineInterpolator _timelineInterpolator;

        public CanBusLogFileTimelineReader(
            ICanBusLogLineParser canBusLogLineParser,
            ICanBusMessageFactory canBusMessageFactory,
            ITimelineInterpolator timelineInterpolator)
        {
            _canBusLogLineParser = canBusLogLineParser ?? throw new ArgumentNullException(nameof(canBusLogLineParser));
            _canBusMessageFactory = canBusMessageFactory ?? throw new ArgumentNullException(nameof(canBusMessageFactory));
            _timelineInterpolator = timelineInterpolator ?? throw new ArgumentNullException(nameof(timelineInterpolator));
        }
        
        public async Task<MessageTimeline> ReadFromCanBusLog(StreamReader reader, bool interpolateTime)
        {
            var timeLine = new MessageTimeline();

            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var parsedLine = _canBusLogLineParser.TryParseLine(line);
                if (parsedLine == null)
                {
                    continue;
                }

                var message = _canBusMessageFactory.Create(CarType.Model3, parsedLine.MessageTypeId, parsedLine.Payload);
                if (message is UnknownMessage)
                {
                    continue;
                }

                var timestamp = TryGetTimestamp(parsedLine, message);

                timeLine.Add(message, timestamp);
            }

            if (interpolateTime)
            {
                _timelineInterpolator.InterpolateTime(timeLine);
            }

            return timeLine;
        }

        private static DateTime? TryGetTimestamp(CanBusLogLine parsedLine, ICanBusMessage message)
        {
            if (parsedLine.UnixTimestamp > 0)
            {
                var seconds = (long) parsedLine.UnixTimestamp;
                var timestamp = DateTimeOffset.FromUnixTimeSeconds(seconds);
                timestamp = timestamp.AddSeconds((double) (parsedLine.UnixTimestamp - seconds));
                return timestamp.UtcDateTime;
            }

            if (message is TimestampMessage timestampMessage)
            {
                return timestampMessage.Timestamp;
            }

            return null;
        }
    }

    public interface ICanBusLogFileTimelineReader
    {
        Task<MessageTimeline> ReadFromCanBusLog(StreamReader reader, bool interpolateTime);
    }
}
