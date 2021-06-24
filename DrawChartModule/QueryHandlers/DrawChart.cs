using DrawChartModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace DrawChartModule.QueryHandlers
{
    public class DrawChart<T>
    {
        public static void Draw(ref Chart chart, TypeOfCharts type, string title, params SeriesDataInPoints<T>[] data)
        {
            var chartData = new ChartEntity<SeriesDataInPoints<T>>(type, title, data);
            DrawChartHandler.Handler<SeriesDataInPoints<T>,T>(ref chart, chartData);
        }
    }
}
