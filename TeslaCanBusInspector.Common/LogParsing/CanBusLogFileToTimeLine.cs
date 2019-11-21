using System;
using System.IO;
using System.Threading.Tasks;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.Timeline;

namespace TeslaCanBusInspector.Common.LogParsing
{
    public class CanBusLogFileToTimeLine : ICanBusLogFileToTimeLine
    {
        private readonly ICanBusLogLineParser _canBusLogLineParser;
        private readonly ICanBusMessageFactory _canBusMessageFactory;

        public CanBusLogFileToTimeLine(
            ICanBusLogLineParser canBusLogLineParser,
            ICanBusMessageFactory canBusMessageFactory)
        {
            _canBusLogLineParser = canBusLogLineParser ?? throw new ArgumentNullException(nameof(canBusLogLineParser));
            _canBusMessageFactory = canBusMessageFactory;
        }
        
        public async Task<MessageTimeLine> ReadFromCanBusLog(StreamReader reader)
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

    public interface ICanBusLogFileToTimeLine
    {
        Task<MessageTimeLine> ReadFromCanBusLog(StreamReader reader);
    }
}
