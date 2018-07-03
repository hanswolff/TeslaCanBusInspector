using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TeslaCanBusInspector.Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            var contentRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!Directory.Exists(Path.Combine(contentRoot, "wwwroot")))
            {
                contentRoot = Directory.GetCurrentDirectory();
            }

            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(contentRoot)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
