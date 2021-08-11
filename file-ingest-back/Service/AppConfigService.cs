using file_ingest_back.Model;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using file_ingest_back.Extensions;
using System;

namespace file_ingest_back.Service
{
    public class AppConfigService : IAppConfigService
    {
        public IConfiguration configuration;
        public AppConfigService(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        public string GetOutPutFolder()
        {
            var path = configuration.GetValue<string>("LocalPath:Path");
            Console.WriteLine($"Save files to: {path}");
            return path;
        }
    }
}
