using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TeslaCanBusInspector.Common.LogParsing;

namespace TeslaCanBusInspector
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            if (args.Length < 1)
            {
                Console.WriteLine("Missing command line arguments.");
                return;
            }

            var path = args[0];

            var services = new ServiceCollection();
            IocConfig.Configure(services);
            var serviceProvider = services.BuildServiceProvider();

            var canBusLogPathReader = serviceProvider.GetRequiredService<ICanBusLogPathReader>();

            await foreach (var timeline in canBusLogPathReader.LoadTimelines(path, false))
            {
                // TODO: process timeline
            }
        }
    }
}
