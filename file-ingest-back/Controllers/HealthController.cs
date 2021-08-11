using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace file_ingest_back.Controllers
{

    [ApiController]
    public class HealthController : BaseController
    {
        public IConfiguration configuration;
        public HealthController(IConfiguration iConfig) : base(iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet]
        [Route("islive")]
        public ActionResult IsLive()
        {
            _logger.Debug("is live OK");
            return Ok("is live OK");
        }
    }
}
