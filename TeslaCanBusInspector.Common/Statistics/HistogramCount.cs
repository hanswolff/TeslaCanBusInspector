using System.Threading;

namespace TeslaCanBusInspector.Common.Statistics
{
    public struct HistogramCount<TKey>
    {
        private long _count;

        public TKey Key { get; }
        public long Count => _count;

        public HistogramCount(TKey key)
        {
            Key = key;
            _count = 0;
        }

        public void IncrementCount()
        {
            Interlocked.Increment(ref _count);
        }
    }
}
