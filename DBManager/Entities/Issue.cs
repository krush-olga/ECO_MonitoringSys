using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Issue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tema { get; set; }
        public DateTime CreationDate { get; set; }

        public Issue(int id)
        {
            this.Id = id;
        }

        public Issue(int id, string name, string description, DateTime creationDate, string seriesId, string tema) : this(id)
        {
            this.Name = name;
            this.Description = description;
            this.CreationDate = creationDate;
            this.Tema = tema;
        }

        public override bool Equals(object obj)
        {
            var issue = obj as Issue;
            return issue != null &&
                   Id == issue.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class IssueMapper
    {
        public static Issue Map(IList<Object> row)
        {
            var i = new Issue(Int32.Parse(row[0].ToString()))
            {
                Name = row[1].ToString(),
                Description = row[2].ToString(),
                CreationDate = DateTime.Parse(row[3].ToString()),
                Tema = row[4].ToString(),
            };

            return i;
        }
    }
}