using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
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

            var sourcePath = args[0];
            var destinationPath = args[1];

            var services = new ServiceCollection();
            IocConfig.Configure(services);
            var serviceProvider = services.BuildServiceProvider();

            var chargingSessionsToCsv = serviceProvider.GetRequiredService<IModel3ChargingSessionsToCsv>();

            await chargingSessionsToCsv.Transform(sourcePath, destinationPath, TimeSpan.FromMinutes(15));
        }
    }
}
