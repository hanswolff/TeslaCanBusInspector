using Microsoft.Extensions.DependencyInjection;
using TeslaCanBusInspector.Common;
using TeslaCanBusInspector.Model3;

namespace TeslaCanBusInspector
{
    public static class IocConfig
    {
        public static void Configure(IServiceCollection services)
        {
            Common.IocConfig.Configure(services);

            services.AddSingleton<IChargingSessionRowWriter, ChargingSessionRowWriter>();
            services.AddSingleton<ICsvRowWriter, CsvRowWriter>();

            services.AddSingleton<IModel3CanBusLogFileToCsv, Model3CanBusLogFileToCsv>();
            services.AddSingleton<IModel3ChargingSessionsToCsv, Model3ChargingSessionsToCsv>();
        }
    }
}