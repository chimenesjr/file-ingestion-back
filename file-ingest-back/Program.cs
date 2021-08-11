using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace file_ingest_back
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            
            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            // tries to get port from environment for cloud run
            string port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            Console.WriteLine($"ENV Port: {port}");
            Console.WriteLine($"ENV: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

            string url = String.Concat("http://0.0.0.0:", port);
            
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext,config) =>
                {
                    var env = builderContext.HostingEnvironment;
                    Console.WriteLine($"ENV: {env.EnvironmentName}");

                    config
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true); // optional extra provider
                    
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls(url)
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    })
                    .UseNLog();
                });
        }
    }
}
