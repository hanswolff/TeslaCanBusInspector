using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TeslaCanBusInspector.Common;
using TeslaCanBusInspector.Common.LogParsing;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.Messages.Model3;

namespace TeslaCanBusInspector.Model3
{
    public class Model3CanBusLogFileToCsv : IModel3CanBusLogFileToCsv
    {
        private readonly ICanBusLogFileTimeLineReader _canBusLogFileTimeLineReader;
        private readonly ICsvRowWriter _csvRowWriter;

        public Model3CanBusLogFileToCsv(
            ICanBusLogFileTimeLineReader canBusLogFileTimeLineReader,
            ICsvRowWriter csvRowWriter)
        {
            _canBusLogFileTimeLineReader = canBusLogFileTimeLineReader ?? throw new ArgumentNullException(nameof(canBusLogFileTimeLineReader));
            _csvRowWriter = csvRowWriter;
        }

        public async Task Transform(string canBusLogFile, string targetCsvFile)
        {
            DateTime lastTimestamp = default;
            CsvRow lastRow = null;
            var row = new CsvRow();

            using var reader = File.OpenText(canBusLogFile);
            var timeLine = await _canBusLogFileTimeLineReader.ReadFromCanBusLog(reader, true);

            await using var writer = File.CreateText(targetCsvFile);

            await _csvRowWriter.WriteHeader(writer);
            var lines = 0;

            foreach (var message in timeLine.Where(m => !(m is UnknownMessage)))
            {
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
                    await _csvRowWriter.WriteLine(writer, row);
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
                case BatteryCapacityMessage m:
                    row.FullBatteryCapacity = m.FullBatteryCapacity;
                    row.ExpectedRemainingCapacity = m.ExpectedRemainingCapacity;
                    return;
                case BatteryPowerMessage m:
                    row.BatteryCurrent = m.BatteryCurrentRaw;
                    row.BatteryVoltage = m.BatteryVoltage;
                    return;
                case ChargeDischargeMessage m:
                    row.TotalCharge = m.TotalCharge;
                    row.TotalDischarge = m.TotalDischarge;
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
                case TemperatureMessage m:
                    row.AmbientTemperature = m.AmbientTempFiltered;
                    return;
            }
        }

        private static void EnrichMemoizedValues(CsvRow row, CsvRow lastRow)
        {
            if (lastRow == null) return;

            row.AmbientTemperature ??= lastRow.AmbientTemperature;
            row.ExpectedRemainingCapacity ??= lastRow.ExpectedRemainingCapacity;
            row.FullBatteryCapacity ??= lastRow.FullBatteryCapacity;
            row.StateOfCharge ??= lastRow.StateOfCharge;
            row.TotalCharge ??= lastRow.TotalCharge;
            row.TotalDischarge ??= lastRow.TotalDischarge;
        }
    }

    public interface IModel3CanBusLogFileToCsv
    {
        Task Transform(string canBusLogFile, string targetCsvFile);
    }
}
