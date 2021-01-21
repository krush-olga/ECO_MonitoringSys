using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Expert
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role role { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ExpertMapper
    {
        public static Expert Map(IList<Object> row)
        {
            var i = new Expert();
            i.Id = Int32.Parse(row[0].ToString());
            i.Name = row[1].ToString();

            return i;
        }
    }
}