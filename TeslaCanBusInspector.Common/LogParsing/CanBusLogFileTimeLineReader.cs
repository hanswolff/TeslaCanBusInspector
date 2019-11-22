using System;
using System.IO;
using System.Threading.Tasks;
using TeslaCanBusInspector.Common.Interpolation;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.Timeline;

namespace TeslaCanBusInspector.Common.LogParsing
{
    public class CanBusLogFileTimeLineReaderReader : ICanBusLogFileTimeLineReader
    {
        private readonly ICanBusLogLineParser _canBusLogLineParser;
        private readonly ICanBusMessageFactory _canBusMessageFactory;
        private readonly ITimeLineInterpolator _timeLineInterpolator;

        public CanBusLogFileTimeLineReaderReader(
            ICanBusLogLineParser canBusLogLineParser,
            ICanBusMessageFactory canBusMessageFactory,
            ITimeLineInterpolator timeLineInterpolator)
        {
            _canBusLogLineParser = canBusLogLineParser ?? throw new ArgumentNullException(nameof(canBusLogLineParser));
            _canBusMessageFactory = canBusMessageFactory ?? throw new ArgumentNullException(nameof(canBusMessageFactory));
            _timeLineInterpolator = timeLineInterpolator ?? throw new ArgumentNullException(nameof(timeLineInterpolator));
        }
        
        public async Task<MessageTimeLine> ReadFromCanBusLog(StreamReader reader, bool interpolateTime)
        {
            var timeLine = new MessageTimeLine();

            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {

                if (string.IsNullOrEmpty(line)) continue;

                var parsedLine = _canBusLogLineParser.TryParseLine(line);
                if (parsedLine == null)
                {
                    continue;
                }

                var message = _canBusMessageFactory.Create(CarType.Model3, parsedLine.MessageTypeId, parsedLine.Payload);

                var timestamp = TryGetTimestamp(parsedLine, message);

                timeLine.Add(message, timestamp);
            }

            if (interpolateTime)
            {
                _timeLineInterpolator.InterpolateTime(timeLine);
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

    public interface ICanBusLogFileTimeLineReader
    {
        Task<MessageTimeLine> ReadFromCanBusLog(StreamReader reader, bool interpolateTime);
    }
}
