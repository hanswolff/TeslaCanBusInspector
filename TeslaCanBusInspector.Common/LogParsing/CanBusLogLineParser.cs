using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace TeslaCanBusInspector.Common.LogParsing
{
    public class CanBusLogLineParser : ICanBusLogLineParser
    {
        private static readonly Regex CanDumpFormat = new Regex(@"^\(\d+.\d+\) [\w\d]+ [A-F0-9]{3}#([A-F0-9]{2})+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex ScanMyTeslaFormat = new Regex(@"^[A-F0-9]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Dictionary<Regex, Func<string, CanBusLogLine>> Actions =
            new Dictionary<Regex, Func<string, CanBusLogLine>>
            {
                {CanDumpFormat, TryParseLineCanDumpLine},
                {ScanMyTeslaFormat, TryParseLineScanMyTeslaLine}
            };

        private static readonly HashSet<string> SkipLines = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "ATCAF0",
            "ATCF 000",
            "ATCM 000",
            "BUFFER",
            "BUFFER FULL",
            "Adapter error ?",
            "Connecting to car...",
            "Using ELM327 command set"
        };

        private Func<string, CanBusLogLine> _lastSuccessfulAction;

        public CanBusLogLine TryParseLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }

            line = line.Trim();
            if (line.Length < 5)
            {
                return null;
            }

            if (SkipLines.Contains(line))
            {
                return null;
            }

            return TryParseTrimmedLine(line);
        }

        private CanBusLogLine TryParseTrimmedLine(string line)
        {
            CanBusLogLine result;
            if (_lastSuccessfulAction != null && (result = _lastSuccessfulAction(line)) != null)
            {
                return result;
            }

            foreach (var (regex, func) in Actions)
            {
                if (!regex.IsMatch(line) || (result = func(line)) == null)
                {
                    continue;
                }

                _lastSuccessfulAction = func;
                return result;
            }

            return null;
        }

        private static CanBusLogLine TryParseLineCanDumpLine(string line)
        {
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

        private static CanBusLogLine TryParseLineScanMyTeslaLine(string line)
        {
            if (line.Length < 5)
            {
                return null;
            }

            var messageTypeHex = line.Substring(0, 3);
            if (!ushort.TryParse(messageTypeHex, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture,
                out var messageTypeId))
            {
                return null;
            }

            var hexString = line.Substring(3);
            if (hexString.Length % 2 != 0)
            {
                return null;
            }

            if (!hexString.All(c => c >= '0' && c <= '9' || c >= 'A' && c <= 'F' || c >= 'a' && c <= 'f'))
            {
                return null;
            }

            var payload = StringToByteArray(hexString);
            return new CanBusLogLine(0, null, messageTypeId, payload);
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
