using System;
using System.Collections;
using System.Collections.Generic;
using TeslaCanBusInspector.Common.Messages;

namespace TeslaCanBusInspector.Common.Timeline
{
    public class MessageTimeLine : IEnumerable<ICanBusMessage>
    {
        private readonly List<ICanBusMessage> _messages = new List<ICanBusMessage>();

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public void Add(ICanBusMessage message, DateTime? timestamp)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            UpdateStartEndTime(timestamp);

            _messages.Add(message);
        }

        private void UpdateStartEndTime(DateTime? timestamp)
        {
            if (timestamp == null)
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

            if (StartTime == default)
            {
                StartTime = timestamp.Value;
            }
        }

        public IEnumerator<ICanBusMessage> GetEnumerator()
        {
            return _messages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
