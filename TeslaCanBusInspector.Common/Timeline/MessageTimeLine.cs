using System;
using System.Collections;
using System.Collections.Generic;
using TeslaCanBusInspector.Common.Messages;

namespace TeslaCanBusInspector.Common.Timeline
{
    public class MessageTimeline : IEnumerable<TimedValue<ICanBusMessage>>
    {
        private readonly List<TimedValue<ICanBusMessage>> _messages = new List<TimedValue<ICanBusMessage>>();

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public void Add(ICanBusMessage message, DateTime? timestamp)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            UpdateEndTimeSequential(timestamp);
            UpdateStartTime(timestamp);

            _messages.Add(new TimedValue<ICanBusMessage>(timestamp, message));
        }

        public void SetTime(int index, DateTime timestamp)
        {
            UpdateEndTime(timestamp);
            UpdateStartTime(timestamp);

            _messages[index].Timestamp = timestamp;
        }

        private void UpdateStartTime(DateTime? timestamp)
        {
            if (timestamp == null || timestamp.Value == default)
            {
                return;
            }

            if (StartTime == default || timestamp < StartTime)
            {
                StartTime = timestamp.Value;
            }
        }

        private void UpdateEndTime(DateTime? timestamp)
        {
            if (timestamp == null|| timestamp.Value == default)
            {
                return;
            }

            if (EndTime == default || EndTime <= timestamp)
            {
                EndTime = timestamp.Value;
            }
        }

        private void UpdateEndTimeSequential(DateTime? timestamp)
        {
            if (timestamp == null || timestamp.Value == default)
            {
                return;
            }

            if (EndTime == default || EndTime <= timestamp)
            {
                EndTime = timestamp.Value;
            }
            else
            {
                throw new InvalidOperationException("CAN messages must be added in sequential time order");
            }
        }

        public IEnumerator<TimedValue<ICanBusMessage>> GetEnumerator()
        {
            return _messages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
