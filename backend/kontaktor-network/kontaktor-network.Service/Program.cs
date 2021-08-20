using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace netcoreservice.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: ƒобавить параметры дл€ выбора окружени€ хостинга
            //Microsoft.AspNetCore.Hosting.WindowsServices.WebHostWindowsServiceExtensions.
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); // TODO: Check Windows-specific

                    var config = new ConfigurationBuilder()
                        //.SetBasePath(Directory.Get())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{environment}.json", true)
                        .AddEnvironmentVariables().Build();

                    
                    webBuilder.ConfigureLogging((logging) =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Trace);
                        logging.AddLog4Net();
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(config["hostUrl"]);
                })
                .UseWindowsService()
                .UseSystemd()
        ;
    }
}
