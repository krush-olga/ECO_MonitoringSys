using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawChartModule.Models
{
    public class ChartEntity<T>
      where T : class
    {
        public TypeOfCharts Type { get; set; }
        public string Title { get; set; }
        public T[] Series { get; set; }
        public ChartEntity(TypeOfCharts type, string title, T[] series)
        {
            Type = type;
            Title = title;
            Series = series;
        }
    }
}
