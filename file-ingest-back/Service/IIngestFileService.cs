using file_ingest_back.Model;

namespace file_ingest_back.Service
{
    public interface IIngestFileService
    {
        void IngestFile(IModel model);
    }
}
