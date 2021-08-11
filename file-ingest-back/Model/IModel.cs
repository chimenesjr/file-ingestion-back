using file_ingest_back.Enum;
using System;
using System.Threading.Tasks;

namespace file_ingest_back.Model
{
    public abstract class IModel
    {
        public IModel()
        {
            created = DateTime.Now;
            Id = Guid.NewGuid();
        }

        private DateTime created;
        public Guid Id { get; set; }
        public string ContextName { get; set; }
        public DateTime Created { get => created; }
        public int time_to_hold { get; set; }
        public abstract FileTypeEnum type { get; }
        public DateTime Finish { get; set; }

        public async Task Process()
        {
            Task.Delay(time_to_hold).Wait();
            Finish = DateTime.Now;
            Done?.Invoke(this, null);
        }

        public event EventHandler Done;
    }
}
