using System;
using file_ingest_back.Controllers;
using file_ingest_back.Model;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace file_ingest_back_tests
{
    [TestFixture]
    public class IngestFileControllerTest
    {
        private IngestFileController controller;
        private SnakeFIle model = new SnakeFIle
            {
                Id = Guid.NewGuid(),
                ContextName = "Name",
            };

        [SetUp]
        public void Setup()
        {
            controller = new IngestFileController(null, null);
        }

        [Test]
        public void Ingest_Get_Return200()
        {
            var response = controller.Ingest();
            Assert.IsInstanceOf<OkObjectResult>(response);
            var result = response as ObjectResult;
            Assert.IsInstanceOf<SnakeFIle>(result.Value);
        }

        [Test]
        public void Ingest_PostNullObject_Return400()
        {
            var response = controller.Ingest();
            Assert.IsInstanceOf<BadRequestResult>(response);
        }
    }
}