using System;
using file_ingest_back.Model;
using file_ingest_back.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace file_ingest_back.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class IngestFileController : BaseController
    {
        IIngestFileService service;
        public IngestFileController(IConfiguration iConfig, IIngestFileService iservice) : base(iConfig)
        {
            service = iservice;
        }

        [HttpGet]
        [Route("ingest")]
        public ActionResult Ingest()
        {
            var model = new SnakeFIle
            {
                Id = Guid.NewGuid(),
                ContextName = "Name",
            };

            return Ok(model);
        }

        [HttpPost]
        [Route("snake")]
        public ActionResult Snake(SnakeFIle model)
        {
            if(model == null)
                return BadRequest();

            service.IngestFile(model);
            return Ok(model);
        }

        [HttpPost]
        [Route("tiger")]
        public ActionResult Tiger(TigerFile model)
        {
            if(model == null)
                return BadRequest();

            service.IngestFile(model);
            return Ok(model);
        }
    }
}
