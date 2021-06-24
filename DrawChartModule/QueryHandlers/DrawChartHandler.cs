using DrawChartModule.Commands;
using DrawChartModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace DrawChartModule.QueryHandlers
{
    public class DrawChartHandler
    {
        public static Chart Handler<T,K>(ref Chart mainChart, ChartEntity<T> chart)
            where T : class
        {
            SeriesDataInPoints<K>[] series;
            DrawChartCommand<K> draw;
            switch (chart.Type)
            {
                case TypeOfCharts.Column:
                    series = chart.Series as SeriesDataInPoints<K>[];
                    draw = new DrawChartCommand<K>();
                    return draw.DrawColumnChart(ref mainChart, series, chart.Title);

                case TypeOfCharts.Bar:
                    series = chart.Series as SeriesDataInPoints<K>[];
                    draw = new DrawChartCommand<K>();
                    return draw.DrawBarChart(ref mainChart, series, chart.Title);

                case TypeOfCharts.Spline:
                    series = chart.Series as SeriesDataInPoints<K>[];
                    draw = new DrawChartCommand<K>();
                    return draw.DrawSplineChart(ref mainChart, series, chart.Title);

                case TypeOfCharts.Pie:
                    series = chart.Series as SeriesDataInPoints<K>[];
                    draw = new DrawChartCommand<K>();
                    return draw.DrawPieChart(ref mainChart, series, chart.Title);

                case TypeOfCharts.Area:
                    series = chart.Series as SeriesDataInPoints<K>[];
                    draw = new DrawChartCommand<K>();
                    return draw.DrawAreaChart(ref mainChart, series, chart.Title);
                case TypeOfCharts.Line:
                    series = chart.Series as SeriesDataInPoints<K>[];
                    draw = new DrawChartCommand<K>();
                    return draw.DrawLineChart(ref mainChart, series, chart.Title);


            }
            return null;
        }
    }
}
