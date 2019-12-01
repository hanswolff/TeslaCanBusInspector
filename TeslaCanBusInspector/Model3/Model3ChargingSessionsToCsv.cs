using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TeslaCanBusInspector.Common;
using TeslaCanBusInspector.Common.LogParsing;
using TeslaCanBusInspector.Common.Messages;
using TeslaCanBusInspector.Common.Session;

namespace TeslaCanBusInspector.Model3
{
    public class Model3ChargingSessionsToCsv : IModel3ChargingSessionsToCsv
    {
        private readonly ICanBusLogPathReader _canBusLogPathReader;
        private readonly IChargingSessionRowWriter _chargingSessionRowWriter;

        public Model3ChargingSessionsToCsv(
            ICanBusLogPathReader canBusLogPathReader,
            IChargingSessionRowWriter chargingSessionRowWriter)
        {
            _canBusLogPathReader = canBusLogPathReader;
            _chargingSessionRowWriter = chargingSessionRowWriter;
        }

        public async Task Transform(string sourcePath, string destinationPath)
        {
            await foreach (var timeline in _canBusLogPathReader.LoadTimelines(sourcePath, false))
            {
                foreach (var chargingSession in new ChargingSessionFilter().GetChargingSessions(timeline))
                {
                    var csvFileName = Path.Combine(destinationPath, GetChargingSessionCsvFileName(chargingSession));
                    await using var writer = File.CreateText(csvFileName);
                    await _chargingSessionRowWriter.WriteHeader(writer);

                    foreach (var timedMessage in chargingSession.Where(m => !(m.Value is UnknownMessage)))
                    {
                        var timestamp = timedMessage.Timestamp ?? default;
                        var message = timedMessage.Value;

                        // TODO: write CSV row
                    }
                }
            }
        }

        private static string GetChargingSessionCsvFileName(MessageTimeline timeline)
        {
            return $"ChargingSession-{timeline.StartTime:O}-{timeline.EndTime:O}.csv";
        }
    }

    public interface IModel3ChargingSessionsToCsv
    {
        Task Transform(string sourcePath, string destinationPath);
    }
}
