using System;
using System.Collections.Generic;
using System.IO;
using TeslaCanBusInspector.Common.Session;

namespace TeslaCanBusInspector.Common.LogParsing
{
    public class CanBusLogPathReader : ICanBusLogPathReader
    {
        private static readonly HashSet<string> AllowedFileExtensions =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".log",
                ".txt"
            };

        private readonly ICanBusLogFileTimelineReader _canBusLogFileTimelineReader;

        public CanBusLogPathReader(
            ICanBusLogFileTimelineReader canBusLogFileTimelineReader)
        {
            _canBusLogFileTimelineReader = canBusLogFileTimelineReader ?? throw new ArgumentNullException(nameof(canBusLogFileTimelineReader));
        }

        public async IAsyncEnumerable<MessageTimeline> LoadTimelines(string path, bool includeSubDirs)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (path.Length <= 3 && path.Contains(':'))
            {
                directoryInfo = directoryInfo.Root;
            }

            foreach (var file in directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories))
            {
                if (!AllowedFileExtensions.Contains(file.Extension))
                {
                    continue;
                }

                using var reader = new StreamReader(file.FullName);

                yield return await _canBusLogFileTimelineReader.ReadFromCanBusLog(reader, true);
            }
        }
    }

    public interface ICanBusLogPathReader
    {
        IAsyncEnumerable<MessageTimeline> LoadTimelines(string path, bool includeSubDirs);
    }
}
