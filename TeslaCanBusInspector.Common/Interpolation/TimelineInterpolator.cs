using System;
using System.Linq;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.Statistics;
using TeslaCanBusInspector.Common.Timeline;

namespace TeslaCanBusInspector.Common.Interpolation
{
    public class TimelineInterpolator : ITimelineInterpolator
    {
        public void InterpolateTime(MessageTimeline timeline)
        {
            if (timeline == null) throw new ArgumentNullException(nameof(timeline));
            
            var messages = timeline.ToList();

            var histogram = new MessageTypeHistogram(messages.Select(m => m.Value));

            var timestampMessagesCount = histogram[TimestampMessage.MessageTypeIdConstant].Count;
            if (timestampMessagesCount < 2)
            {
                return;
            }

            var mostFrequentMessageTypes = histogram
                .Where(mtc => mtc.Count > timestampMessagesCount)
                .OrderByDescending(mtc => mtc.Count)
                .Select(mtc => mtc.Key)
                .ToList();
            if (!mostFrequentMessageTypes.Any())
            {
                return;
            }

            var messageTypeForInterpolation = mostFrequentMessageTypes[0];

            ITimestampMessage lastTimestampMessage = null;
            var lastTimestampMessageIndex = 0;
            for (var i = 0; i < messages.Count; i++)
            {
                var message = messages[i];
                if (!(message.Value is ITimestampMessage timestampMessage))
                {
                    continue;
                }
                
                if (lastTimestampMessage == null)
                {
                    lastTimestampMessage = timestampMessage;
                    lastTimestampMessageIndex = i;
                    continue;
                }

                var timeDiff = timestampMessage.Timestamp - lastTimestampMessage.Timestamp;
                if (timeDiff > TimeSpan.FromSeconds(3) || timeDiff == TimeSpan.Zero)
                {
                    lastTimestampMessage = timestampMessage;
                    lastTimestampMessageIndex = i;
                    continue;
                }

                var rangeHistogram = new MessageTypeHistogram(
                    Enumerable.Range(lastTimestampMessageIndex + 1, i - lastTimestampMessageIndex).Select(j => messages[j].Value));
                var timeToDivideBy = rangeHistogram[messageTypeForInterpolation].Count + 1;
                var timeSlice = timeDiff / timeToDivideBy;
                var interpolatedTime = lastTimestampMessage.Timestamp;

                for (var j = lastTimestampMessageIndex + 1; j < i; j++)
                {
                    var rangeMessage = messages[j];
                    if (rangeMessage.Value.MessageTypeId != messageTypeForInterpolation)
                    {
                        continue;
                    }

                    interpolatedTime += timeSlice;
                    timeline.SetTime(j, interpolatedTime);
                }

                lastTimestampMessage = timestampMessage;
                lastTimestampMessageIndex = i;
            }
        }
    }

    public interface ITimelineInterpolator
    {
        void InterpolateTime(MessageTimeline timeline);
    }
}
