using Microsoft.Extensions.DependencyInjection;
using TeslaCanBusInspector.Common.LogParsing;

namespace TeslaCanBusInspector.Common
{
    public static class IocConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<ICanBusLogFileToTimeLine, CanBusLogFileToTimeLine>();
            services.AddSingleton<ICanBusLogLineParser, CanBusLogLineParser>();
            services.AddSingleton<ICanBusMessageFactory, CanBusMessageFactory>();
        }
    }
}