using Microsoft.Extensions.DependencyInjection;
using TeslaCanBusInspector.Common.Interpolation;
using TeslaCanBusInspector.Common.LogParsing;

namespace TeslaCanBusInspector.Common
{
    public static class IocConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<ICanBusLogFileTimeLineReader, CanBusLogFileTimeLineReaderReader>();
            services.AddSingleton<ICanBusLogLineParser, CanBusLogLineParser>();
            services.AddSingleton<ICanBusMessageFactory, CanBusMessageFactory>();
            services.AddSingleton<ITimeLineInterpolator, TimeLineInterpolator>();
        }
    }
}