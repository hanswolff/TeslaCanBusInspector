using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TeslaCanBusInspector.Common;
using TeslaCanBusInspector.Common.LogParsing;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.Messages.Model3;
using TeslaCanBusInspector.Common.Session;
using TeslaCanBusInspector.Configuration;

namespace TeslaCanBusInspector.Model3
{
    public class Model3ChargingSessionsToCsv : IModel3ChargingSessionsToCsv
    {
        private readonly ICanBusLogPathReader _canBusLogPathReader;
        private readonly IChargingSessionRowWriter _rowWriter;

        public Model3ChargingSessionsToCsv(
            ICanBusLogPathReader canBusLogPathReader,
            IChargingSessionRowWriter rowWriter)
        {
            _canBusLogPathReader = canBusLogPathReader;
            _rowWriter = rowWriter;
        }

        public async Task Transform(string sourcePath, string destinationPath, ChargingSessionTransformationOptions options)
        {
            var tasks = new List<Task>();

            await foreach (var timeline in _canBusLogPathReader.LoadTimelines(sourcePath, options.IncludeSubdirectories))
            {
                Console.Write('.');

                tasks.Add(Task.Run(async () => {
                    try
                    {
                        await ProcessTimeline(destinationPath, timeline, options);
                    }
                    catch (Exception ex)
                    {
                        await Console.Error.WriteLineAsync(ex.ToString());
                    }
                }));
            }

            await Task.WhenAll(tasks);
            Console.WriteLine();
        }

        private async Task ProcessTimeline(string destinationPath, MessageTimeline timeline, ChargingSessionTransformationOptions options)
        {
            foreach (var chargingSession in new ChargingSessionFilter().GetChargingSessions(timeline))
            {
                if (chargingSession.StartTime == default || chargingSession.EndTime == default)
                {
                    continue;
                }

                if (options.MinimumChargingSessionDuration > TimeSpan.Zero &&
                    chargingSession.EndTime - chargingSession.StartTime < options.MinimumChargingSessionDuration)
                {
                    continue;
                }

                var csvFileName = GetChargingSessionCsvFileName(destinationPath, chargingSession);
                await using var writer = File.CreateText(csvFileName);
                await _rowWriter.WriteHeader(writer);

                DateTime lastTimestamp = default;
                ChargingSessionRow lastRow = null;
                var row = new ChargingSessionRow();

                foreach (var timedMessage in chargingSession.Where(m => !(m.Value is UnknownMessage)))
                {
                    var timestamp = timedMessage.Timestamp ?? default;
                    var message = timedMessage.Value;

                    ParseMessage(message, row);

                    if (timestamp == default || timestamp == lastTimestamp)
                    {
                        continue;
                    }

                    if (lastTimestamp == default)
                    {
                        lastTimestamp = timestamp;
                        row = new ChargingSessionRow
                        {
                            Timestamp = timestamp
                        };
                        continue;
                    }

                    EnrichMemoizedValues(row, lastRow);

                    if (row.ShouldWriteRow)
                    {
                        await _rowWriter.WriteLine(writer, row);
                    }

                    lastTimestamp = timestamp;
                    lastRow = row;
                    row = new ChargingSessionRow
                    {
                        Timestamp = timestamp
                    };
                }
            }
        }

        private static string GetChargingSessionCsvFileName(string path, MessageTimeline timeline)
        {
            var fileName = $"ChargingSession-{timeline.StartTime:yyyyMMdd-HHmmss}-{timeline.EndTime:yyyyMMdd-HHmmss}.csv";
            return Path.Combine(path, fileName);
        }

        private static void ParseMessage(ICanBusMessage message, ChargingSessionRow row)
        {
            switch (message)
            {
                case BatteryInfoMessage m:
                    row.BmsChargeStatus = m.BmsChargeStatus;
                    row.BmsState = m.BmsState;
                    row.CellTemperature = m.MinBatteryTemperature;
                    row.MaxChargePower = m.BmsChargePowerAvailable;
                    return;
                case BatteryPowerMessage m:
                    row.BatteryCurrent = m.BatteryCurrentRaw;
                    row.BatteryVoltage = m.BatteryVoltage;
                    return;
                case StateOfChargeMessage m:
                    row.StateOfCharge = m.StateOfChargeMin;
                    return;
            }
        }

        private static void EnrichMemoizedValues(ChargingSessionRow row, ChargingSessionRow lastRow)
        {
            if (lastRow == null) return;

            row.BmsChargeStatus ??= lastRow.BmsChargeStatus;
            row.BmsState ??= lastRow.BmsState;
            row.CellTemperature ??= lastRow.CellTemperature;
            row.MaxChargePower ??= lastRow.MaxChargePower;
            row.StateOfCharge ??= lastRow.StateOfCharge;
        }
    }

    public interface IModel3ChargingSessionsToCsv
    {
        Task Transform(string sourcePath, string destinationPath, ChargingSessionTransformationOptions options);
    }
}
