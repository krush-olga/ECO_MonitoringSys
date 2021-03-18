using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Expert
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }

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
            
            if (row.Count > 2)
            {
                i.Role = (Role)Int32.Parse(row[2].ToString()); 
            }

            return i;
        }
    }
}