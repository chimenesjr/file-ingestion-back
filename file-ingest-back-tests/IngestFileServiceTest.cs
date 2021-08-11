using file_ingest_back.Model;
using file_ingest_back.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace file_ingest_back_tests
{
    [TestFixture]
    public class IngestFileServiceTest
    {
        internal Mock<IAppConfigService> appConfig;
        internal IngestFileService service;
        private List<IModel> finished;
        [SetUp]
        public void SetUp()
        {
            appConfig = new Mock<IAppConfigService>();
            service = new IngestFileService(appConfig.Object);
            finished = new List<IModel>();
            setupMock();
        }

        [Test]
        public void IngestFile_IngestTiger()
        {
            var list = GetList();

            GetList().ForEach(model =>
            {
                model.Done += Model_Done;

                service.IngestFile(model);
            });

            Thread.Sleep(30 * 1000);

            Assert.IsInstanceOf<SnakeFIle>(finished.First());
        }

        private void Model_Done(object sender, EventArgs e)
        {
            finished.Add(sender as IModel);
        }

        internal List<IModel> GetList()
        {
            return new List<IModel>
            {
                new TigerFile
                {
                    ContextName = "first tiger",
                    Id = Guid.NewGuid(),
                },
                new SnakeFIle
                {
                    ContextName = "first Snake",
                    Id = Guid.NewGuid(),
                }
            };
        }

        private void setupMock()
        {
            appConfig.Setup(x => x.GetOutPutFolder()).Returns("to-delete-result-from-unit-tests");
        }
    }
}
