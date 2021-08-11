using file_ingest_back.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace file_ingest_back_tests
{
    public class HealthControllerTests
    {
        private HealthController controller;

        [SetUp]
        public void Setup()
        {
            controller = new HealthController(null);
        }

        [Test]
        public void Islive_Get_Return200()
        {
            var response = controller.IsLive();
            var result = response as ObjectResult;
            Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
            Assert.AreEqual(result.Value, "is live OK");
        }
    }
}