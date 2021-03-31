using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Document
    {
        public int event_id { get; set; }
        public string document_code { get; set; }
        public string description { get; set; }

        public override string ToString()
        {
            return description;
        }
    }

    public class DocumentMapper
    {
        public static Document Map(IList<Object> row)
        {
            var d = new Document
            {
                event_id = Int32.Parse(row[0].ToString()),
                document_code = row[1].ToString(),
                description = row[2].ToString()
            };
            return d;
        }
    }
}