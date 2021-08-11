using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using file_ingest_back.Enum;

namespace file_ingest_back.Model
{
    public class TigerFile : IModel
    {
        public override FileTypeEnum type => FileTypeEnum.Tiger;

        public bool is_bengal {get;set;}
        
        public void Roar()
        {

        }
    }
}
