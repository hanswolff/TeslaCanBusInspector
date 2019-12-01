using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TeslaCanBusInspector.Common
{
    public class CsvRowWriter : ICsvRowWriter
    {
        private static readonly Dictionary<string, Func<CsvRow, string>> Properties =
            new Dictionary<string, Func<CsvRow, string>>
            {
                { nameof(CsvRow.Timestamp), row => '"' + row.Timestamp.ToString("o") + '"' },
                { nameof(CsvRow.BmsState), row => row.BmsState?.ToString() },
                { nameof(CsvRow.BmsChargeStatus), row => row.BmsChargeStatus?.ToString() },
                { nameof(CsvRow.StateOfCharge), row => row.StateOfCharge?.Value.ToString("F1") },
                { nameof(CsvRow.FullBatteryCapacity), row => row.FullBatteryCapacity?.Value.ToString("F1") },
                { nameof(CsvRow.ExpectedRemainingCapacity), row => row.ExpectedRemainingCapacity?.Value.ToString("F1") },
                { nameof(CsvRow.TotalDischarge), row => row.TotalDischarge?.Value.ToString("F3") },
                { nameof(CsvRow.TotalCharge), row => row.TotalDischarge?.Value.ToString("F3") },
                { nameof(CsvRow.BatteryVoltage), row => row.BatteryVoltage?.Value.ToString("F1") },
                { nameof(CsvRow.BatteryCurrent), row => row.BatteryCurrent?.Value.ToString("F1") },
                { nameof(CsvRow.Speed), row => row.Speed?.Value.ToString("F1") },
                { nameof(CsvRow.Odometer), row => row.Odometer?.Value.ToString("F3") }
            };

        public async Task WriteHeader(StreamWriter writer)
        {
            var sb = new StringBuilder();

            foreach (var property in Properties)
            {
                if (sb.Length > 0) sb.Append(',');
                sb.Append(property.Key);
            }

            await writer.WriteLineAsync(sb.ToString());
        }

        public async Task WriteLine(StreamWriter writer, CsvRow row)
        {
            var sb = new StringBuilder();

            foreach (var property in Properties)
            {
                if (sb.Length > 0) sb.Append(',');
                sb.Append(property.Value(row));
            }

            await writer.WriteLineAsync(sb.ToString());
        }
    }

    public interface ICsvRowWriter
    {
        Task WriteHeader(StreamWriter writer);
        Task WriteLine(StreamWriter writer, CsvRow row);
    }
}