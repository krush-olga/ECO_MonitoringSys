using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class OwerType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public OwerType() : this(0, string.Empty)
        { }
        public OwerType(int id) : this(id, string.Empty)
        { }
        public OwerType(int id, string type)
        {
            Id = id;
            Type = type;
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
