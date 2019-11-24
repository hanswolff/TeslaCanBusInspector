using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TeslaCanBusInspector.Common.Messages;

namespace TeslaCanBusInspector.Common.Statistics
{
    public class MessageTypeHistogram : IEnumerable<HistogramCount<ushort>>
    {
        private readonly HistogramCount<ushort>[] _messageTypesCount = Enumerable.Range(0, ushort.MaxValue)
            .Select(i => new HistogramCount<ushort>((ushort) i)).ToArray();

        public HistogramCount<ushort> this[ushort messageType] => _messageTypesCount[messageType];

        public MessageTypeHistogram()
        {
        }

        public MessageTypeHistogram(IEnumerable<ICanBusMessage> messages)
        {
            AddRange(messages);
        }

        public void Add(ICanBusMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            _messageTypesCount[message.MessageTypeId].IncrementCount();
        }

        public void AddRange(IEnumerable<ICanBusMessage> messages)
        {
            if (messages == null) return;

            foreach (var message in messages)
            {
                Add(message);
            }
        }

        public IEnumerator<HistogramCount<ushort>> GetEnumerator()
        {
            return _messageTypesCount.Select(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
