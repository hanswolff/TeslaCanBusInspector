using System;
using System.Collections;
using System.Collections.Generic;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.Messages.Model3;

namespace TeslaCanBusInspector.Common.Session
{
    public class MessageTimeline : IEnumerable<TimedValue<ICanBusMessage>>
    {
        private readonly List<TimedValue<ICanBusMessage>> _messages = new List<TimedValue<ICanBusMessage>>();

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public TimedValue<ICanBusMessage> this[int index] => _messages[index];

        public MessageTimeline()
        {
        }

        public MessageTimeline(IEnumerable<ICanBusMessage> messages)
        {
            foreach (var message in messages)
            {
                if (message is TimestampMessage timestampMessage && timestampMessage.IsValid)
                {
                    Add(message, timestampMessage.Timestamp);
                }
                else
                {
                    Add(message, null);
                }
            }
        }

        public void Add(TimedValue<ICanBusMessage> message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            UpdateEndTimeSequential(message.Timestamp);
            UpdateStartTime(message.Timestamp);

            _messages.Add(message);
        }

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
