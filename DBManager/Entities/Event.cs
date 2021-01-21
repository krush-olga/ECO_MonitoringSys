using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LawyerVer { get; set; }
        public string DmVer { get; set; }
        public int UserId { get; set; }
        public int IssueId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class EventMapper
    {
        public static Event Map(IList<Object> row)
        {
            var e = new Event
            {
                Id = Int32.Parse(row[0].ToString()),
                Name = row[1].ToString(),
                Description = row[2].ToString(),
                LawyerVer = row[3]?.ToString(),
                DmVer = row[4]?.ToString(),
                UserId = row[5].ToString().Length != 0 ? Int32.Parse(row[5].ToString()) : -1,
                IssueId = row[6].ToString().Length != 0 ? Int32.Parse(row[6].ToString()) : -1
            };

            return e;
        }
    }

    public class EventTemplateMapper
    {
        public static Event Map(List<Object> row)
        {
            var e = new Event
            {
                Id = Int32.Parse(row[0].ToString()),
                Name = row[1].ToString(),
                Description = row[2].ToString()
            };

            return e;
        }
    }
}