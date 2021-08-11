using file_ingest_back.Model;
using System.Threading.Tasks;
using file_ingest_back.Extensions;

namespace file_ingest_back.Service
{
    public class IngestFileService : IIngestFileService
    {
        public IAppConfigService appConfig;
        public NLog.Logger _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        public IngestFileService(IAppConfigService iConfig)
        {
            appConfig = iConfig;
        }
        public void IngestFile(IModel model)
        {
            try
            {
                Task.Run(() =>
                {
                    this.ProcessFile(model);
                });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private async void ProcessFile(IModel model)
        {
            _logger.Info($"Thread START: {Task.CurrentId} - {model.ContextName} - {model.type} - {model.Id}");

            await model.Process();
            
            model.SaveFile(appConfig.GetOutPutFolder());

            _logger.Info($"Thread END: {Task.CurrentId} - {model.ContextName} - {model.type} - {model.Id}");
        }
    }
}
