using System;
using System.Globalization;
using System.Threading;

namespace TeslaCanBusInspector
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            if (args.Length == 0) return;

            CanBusLogFileToJson.ReadFileToJson(args[0]);
        }
    }
}
