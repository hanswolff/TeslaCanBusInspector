using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using TeslaCanBusInspector.Common.Sockets;

namespace TeslaCanBusInspector
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            //TestSockets().Wait();

            if (args.Length == 0)
            {
                Console.WriteLine("Missing command line arguments.");
                return;
            }

            var canFile = args[0];
            CanBusLogFileToJson.ReadFileToJson(canFile);
        }

        private static async Task TestSockets()
        {
            using (var socketCanReader = new SocketCanReader())
            {
                await socketCanReader.ConnectAsync("localhost", 28700).ConfigureAwait(false);

                Console.ReadLine();
            }
        }
    }
}
