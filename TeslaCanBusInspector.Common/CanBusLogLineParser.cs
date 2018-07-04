using System;
using System.Globalization;
using System.Linq;

namespace TeslaCanBusInspector.Common
{
    public class CanBusLogLineParser : ICanBusLogLineParser
    {
        public CanBusLogLine TryParseLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return null;
            }

            var parts = line.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
            {
                return null;
            }

            if (!decimal.TryParse(parts[0].TrimStart('(').TrimEnd(')'), NumberStyles.Any, CultureInfo.InvariantCulture, out var unixTimestamp))
            {
                return null;
            }

            var messagePayloadParts = parts[2].Split('#');
            if (messagePayloadParts.Length != 2)
            {
                return null;
            }

            var messageTypeHex = messagePayloadParts[0];
            if (!ushort.TryParse(messageTypeHex, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture,
                out var messageTypeId))
            {
                return null;
            }

            return new CanBusLogLine(unixTimestamp, parts[1], messageTypeId, StringToByteArray(messagePayloadParts[1]));
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }

    public interface ICanBusLogLineParser
    {
        CanBusLogLine TryParseLine(string line);
    }
}
