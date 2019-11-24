namespace TeslaCanBusInspector.Common
{
    public class CurrentLastValue<T>
    {
        public T Current { get; private set; }
        public T Last { get; private set; }

        public void SetCurrent(T value)
        {
            Last = Current;
            Current = value;
        }
    }
}
