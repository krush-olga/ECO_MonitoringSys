using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class CalculationSeries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CalculationSeries()
        {
        }

        public CalculationSeries(int id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class CalculatoinSeriesMapper
    {
        public static CalculationSeries Map(IList<Object> row)
        {
            var calc = new CalculationSeries();
            calc.Id = Int32.Parse(row[0].ToString());
            calc.Name = row[1].ToString();
            calc.Description = row[2].ToString();

            return calc;
        }
    }
}