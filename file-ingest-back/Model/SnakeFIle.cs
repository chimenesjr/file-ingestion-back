using System;
using file_ingest_back.Enum;

namespace file_ingest_back.Model
{
    public class SnakeFIle : IModel
    {
        public override FileTypeEnum type => FileTypeEnum.Snake;

        public int? length {get;set;}

        public void Bite()
        {

        }
    }
}