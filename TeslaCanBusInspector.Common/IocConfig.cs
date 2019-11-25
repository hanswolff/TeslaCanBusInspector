using Microsoft.Extensions.DependencyInjection;
using TeslaCanBusInspector.Common.Interpolation;
using TeslaCanBusInspector.Common.LogParsing;
using TeslaCanBusInspector.Common.Session;

namespace TeslaCanBusInspector.Common
{
    public static class IocConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<ICanBusLogFileTimelineReader, CanBusLogFileTimelineReader>();
            services.AddSingleton<ICanBusLogLineParser, CanBusLogLineParser>();
            services.AddSingleton<ICanBusLogPathReader, CanBusLogPathReader>();
            services.AddSingleton<ICanBusMessageFactory, CanBusMessageFactory>();
            services.AddSingleton<IChargingSessionFilter, ChargingSessionFilter>();
            services.AddSingleton<ITimelineInterpolator, TimelineInterpolator>();
        }
    }
}