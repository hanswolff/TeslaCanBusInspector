using System;
using System.IO;
using System.Threading.Tasks;
using TeslaCanBusInspector.Common;
using TeslaCanBusInspector.Common.LogParsing;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.Messages.Model3;

namespace TeslaCanBusInspector.Model3
{
    public class Model3CanBusLogFileToCsv
    {
        public static async Task Transform(string canBusLogFile, string targetCsvFile)
        {
            var csvRowWriter = new CsvRowWriter();
            var parser = new CanBusLogLineParser();
            var messageFactory = new CanBusMessageFactory();

            DateTime lastTimestamp = default;
            CsvRow lastRow = null;
            var row = new CsvRow();

            using var reader = File.OpenText(canBusLogFile);
            await using var writer = File.CreateText(targetCsvFile);

            await csvRowWriter.WriteHeader(writer);
            var lines = 0;

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) continue;

                var parsedLine = parser.TryParseLine(line);
                if (parsedLine == null)
                {
                    continue;
                }

                var message = messageFactory.Create(CarType.Model3, parsedLine.MessageTypeId, parsedLine.Payload);
                if (message is UnknownMessage)
                {
                    continue;
                }

                ParseMessage(message, row);

                if (message is TimestampMessage timestampMessage)
                {
                    if (lastTimestamp == default)
                    {
                        lastTimestamp = timestampMessage.Timestamp;
                        row = new CsvRow();
                        continue;
                    }

                    if (timestampMessage.Timestamp == lastTimestamp)
                    {
                        continue;
                    }

                    EnrichMemoizedValues(row, lastRow);
                    await csvRowWriter.WriteLine(writer, row);
                    if (lines++ % 100 == 0)
                    {
                        Console.Write('.');
                    }

                    lastTimestamp = timestampMessage.Timestamp;
                    lastRow = row;
                    row = new CsvRow
                    {
                        Timestamp = timestampMessage.Timestamp
                    };
                }
            }
            Console.WriteLine();
        }

        private static void ParseMessage(ICanBusMessage message, CsvRow row)
        {
            switch (message)
            {
                case BatteryPowerMessage m:
                    row.BatteryCurrent = m.BatteryCurrentRaw;
                    row.BatteryVoltage = m.BatteryVoltage;
                    return;
                case OdometerMessage m:
                    row.Odometer = m.Odometer;
                    return;
                case SpeedMessage m:
                    row.Speed = m.SignedSpeed;
                    return;
                case StateOfChargeMessage m:
                    row.StateOfCharge = m.StateOfChargeMin;
                    return;
            }
        }

        private static void EnrichMemoizedValues(CsvRow row, CsvRow lastRow)
        {
            if (lastRow == null) return;

            row.StateOfCharge ??= lastRow.StateOfCharge;
        }
    }
}
