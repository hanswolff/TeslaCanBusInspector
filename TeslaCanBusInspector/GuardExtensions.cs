using System;

namespace TeslaCanBusInspector
{
    internal static class GuardExtensions
    {
        public static void RequireBytes(this byte[] payload, int numberOfBytes)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (payload.Length < numberOfBytes)
            {
                throw new ArgumentException($"{nameof(payload)} needs at least {numberOfBytes} bytes");
            }
        }
    }
}
