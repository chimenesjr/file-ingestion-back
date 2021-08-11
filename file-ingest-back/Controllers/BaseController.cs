using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace file_ingest_back.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public NLog.Logger _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        public IConfiguration configuration;
        
        public BaseController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
    }
}