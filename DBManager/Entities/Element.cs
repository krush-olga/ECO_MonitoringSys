using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Element
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Measure { get; set; }
        public bool Rigid { get; set; }
        public bool Voc { get; set; }
        public bool Hydrocarbon { get; set; }
        public string Formula { get; set; }
        public string Cas { get; set; }

        public override string ToString()
        {
            return ToString("G");
        }

        public string ToString(string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return Name;
            }

            switch (format.ToUpper())
            {
                case "NM":
                    return $"{Name} ({Measure})";
                case "N":
                case "G":
                default:
                    return Name;
            }
        }
    }
}
