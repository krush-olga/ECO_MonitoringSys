using DrawChartModule.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace DrawChartModule.Commands
{
    public class DrawChartCommand<T>
    {
        public Chart DrawColumnChart(ref Chart chart, SeriesDataInPoints<T>[] data, string title)
        {
            chart.Titles.Add(title);

            foreach (var item in data)
            {
                var series = chart.Series.Add(item.SeriesName);
                series.ChartType = SeriesChartType.Column;
                foreach (var point in item.Points)
                {
                    series.Points.AddXY(point.X, point.Y);
                }
            }
            return chart;
        }

        public Chart DrawBarChart(ref Chart chart, SeriesDataInPoints<T>[] data, string title)
        {
            chart.Titles.Add(title);

            foreach (var item in data)
            {
                var series = chart.Series.Add(item.SeriesName);
                series.ChartType = SeriesChartType.Bar;
                foreach (var point in item.Points)
                {
                    series.Points.AddXY(point.X, point.Y);
                }
            }
            return chart;
        }

        public Chart DrawSplineChart(ref Chart chart, SeriesDataInPoints<T>[] data, string title)
        {
            chart.Titles.Add(title);

            foreach (var item in data)
            {
                var series = chart.Series.Add(item.SeriesName);
                series.ChartType = SeriesChartType.Spline;
                foreach (var point in item.Points)
                {
                    series.Points.AddXY(point.X, point.Y);
                }
            }
            return chart;
        }

        public Chart DrawPieChart(ref Chart chart, SeriesDataInPoints<T>[] data, string title)
        {
            chart.Titles.Add(title);

            foreach (var item in data)
            {
                var series = chart.Series.Add(item.SeriesName);
                series.ChartType = SeriesChartType.Pie;
                foreach (var point in item.Points)
                {
                    series.Points.AddXY(point.X, point.Y);
                }
            }
            return chart;
        }

        public Chart DrawAreaChart(ref Chart chart, SeriesDataInPoints<T>[] data, string title)
        {
            chart.Titles.Add(title);

            foreach (var item in data)
            {
                var series = chart.Series.Add(item.SeriesName);
                series.ChartType = SeriesChartType.Area;
                foreach (var point in item.Points)
                {
                    series.Points.AddXY(point.X, point.Y);
                }
            }
            return chart;
        }

     

        public Chart DrawLineChart(ref Chart chart, SeriesDataInPoints<T>[] data, string title)
        {
            chart.Titles.Add(title);

            foreach (var item in data)
            {
                var series = chart.Series.Add(item.SeriesName);
                var seriesPoint = chart.Series.Add(item.SeriesName+" point");
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                if (item.Points.FirstOrDefault().X.GetType().Name!="Double")
                {
                    series.YValueType = ChartValueType.Date;
                }
                else
                {
                    seriesPoint.ChartType = SeriesChartType.Point;
                    seriesPoint.Color = Color.Red;
                    seriesPoint.BorderWidth = 10;
                }              
                foreach (var point in item.Points)
                {
                    if (item.Points.FirstOrDefault().X.GetType().Name == "Double")
                        seriesPoint.Points.AddXY(point.X, point.Y);
                    series.Points.AddXY(point.X, point.Y);
                    
                }
            }
            return chart;
        }


    }
}
