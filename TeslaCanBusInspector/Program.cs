using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using TeslaCanBusInspector.Model3;

namespace TeslaCanBusInspector
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            if (args.Length < 2)
            {
                Console.WriteLine("Missing command line arguments.");
                return;
            }

            var canFile = args[0];
            var csvFile = args[1];
            await Model3CanBusLogFileToCsv.Transform(canFile, csvFile);
        }
    }
}
