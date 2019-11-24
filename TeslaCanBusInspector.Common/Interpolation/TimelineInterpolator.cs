using System;
using System.Linq;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.Session;
using TeslaCanBusInspector.Common.Statistics;

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

            var messageTypeForInterpolation = 
                mostFrequentMessageTypes.Contains(BatteryPowerMessage.MessageTypeIdConstant) 
                    ? BatteryPowerMessage.MessageTypeIdConstant
                    : mostFrequentMessageTypes[0];

            var timestampMessages = new CurrentLastValue<ITimestampMessage>();
            var timestampIndexes = new CurrentLastValue<int>();
            for (var i = 0; i < messages.Count; i++)
            {
                var message = messages[i];
                if (!(message.Value is ITimestampMessage timestampMessage))
                {
                    continue;
                }

                timestampMessages.SetCurrent(timestampMessage);
                timestampIndexes.SetCurrent(i);

                if (timestampMessages.Last == null)
                {
                    continue;
                }

                var timeDiff = timestampMessage.Timestamp - timestampMessages.Last.Timestamp;
                if (timeDiff > TimeSpan.FromSeconds(10) || timeDiff == TimeSpan.Zero)
                {
                    continue;
                }

                var rangeHistogram = new MessageTypeHistogram(
                    Enumerable.Range(timestampIndexes.Last + 1, i - timestampIndexes.Last).Select(j => messages[j].Value));
                var timeToDivideBy = rangeHistogram[messageTypeForInterpolation].Count + 1;
                var timeSlice = timeDiff / timeToDivideBy;
                var interpolatedTime = timestampMessages.Last.Timestamp;

                for (var j = timestampIndexes.Last + 1; j < i; j++)
                {
                    var rangeMessage = messages[j];
                    if (rangeMessage.Value.MessageTypeId != messageTypeForInterpolation)
                    {
                        continue;
                    }

                    interpolatedTime += timeSlice;
                    timeline.SetTime(j, interpolatedTime);
                }
            }
        }
    }

    public interface ITimelineInterpolator
    {
        void InterpolateTime(MessageTimeline timeline);
    }
}
