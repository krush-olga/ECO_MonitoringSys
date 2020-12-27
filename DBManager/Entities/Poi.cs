using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Poi
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public int OwnerType { get; set; }
        public int IssueId { get; set; }
        public int SeriesId { get; set; }
        public int FormulaId { get; set; }
        public int EnvironmentId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
