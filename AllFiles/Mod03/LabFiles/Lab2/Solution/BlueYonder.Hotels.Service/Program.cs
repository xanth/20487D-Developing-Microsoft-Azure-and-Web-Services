using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BlueYonder.Hotels.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                                        .AddCommandLine(args)
                                        .Build();

            string userServicePreference = config["mode"];

            IWebHostBuilder builder = CreateWebHostBuilder(args);

            if (string.Equals(userServicePreference, "HttpSys", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Running with HttpSys.");
                builder.UseHttpSys();
            }
            else if (string.Equals(userServicePreference, "Kestrel", StringComparison.OrdinalIgnoreCase))
            {
                // Kestrel does not support windows authentication: use HttpSys or host on IIS or IIS Express
                Console.WriteLine("Running with Kestrel.");
                builder.UseKestrel();
            }
            else
            {
                // IIS does not support HttpSys: must run with Kestrel when hosting with IIS or IIS Express
                Console.WriteLine("Running with IIS Express.");
                builder.UseIISIntegration();

            }

            builder.Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
