using DrawChartModule.Models;
using DrawChartModule.QueryHandlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserMap
{
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();
        }

        private void ChartForm_Load(object sender, EventArgs e)
        {

        }

        public void DrawChartUI(TypeOfCharts type, string title, Dictionary<string, CustomPoint<double>> infoForTable, params SeriesDataInPoints<DateTime>[] data)
        {
            chart1.Visible = false;
            chart2.Visible = false;
            chart3.Visible = false;

            this.chart1.ChartAreas[0].AxisY.Title = "Серії розрахунків";
            this.chart1.ChartAreas[0].AxisX.Title = "Значення";
            this.chart2.ChartAreas[0].AxisY.Title = "Серії розрахунків";
            this.chart2.ChartAreas[0].AxisX.Title = "Значення";
            this.chart3.ChartAreas[0].AxisY.Title = "Серії розрахунків";
            this.chart3.ChartAreas[0].AxisX.Title = "Значення";
           

            
            DrawChart<DateTime>.Draw(ref chart1, TypeOfCharts.Spline, title, data);
            DrawChart<DateTime>.Draw(ref chart2, TypeOfCharts.Bar, title, data);
            DrawChart<DateTime>.Draw(ref chart3, TypeOfCharts.Column, title, data);                     
            

            if (type == TypeOfCharts.Bar)
                checkBox1.Checked = true;
            if (type == TypeOfCharts.Pie)
                checkBox3.Checked = true;          
            if (type == TypeOfCharts.Line)
            {
                chart1.Visible = true;
                checkBox2.Checked = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                chart1.Visible = true;
                chart3.Visible = false;
                chart2.Visible = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                chart1.Visible = false;
                chart3.Visible = false;
                chart2.Visible = true;
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                chart1.Visible = false;
                chart3.Visible = true;
                chart2.Visible = false;
                checkBox2.Checked = false;
                checkBox1.Checked = false;
            }
        }
    }
}
