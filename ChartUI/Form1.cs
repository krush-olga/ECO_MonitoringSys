using Data;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartUI
{
    public partial class Form1 : Form
    {
        private readonly TypeOfCharts type;
        private readonly string title;
        private readonly SeriesDataInPoints<double>[] data;
        private DBManager dbManager = new DBManager();
        private List<Chart> listOfCharts = new List<Chart>();
        SeriesDataInPoints<double>[] dataChart;
        string titleChart;
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        public void DrawChartUI(TypeOfCharts type, string title, Dictionary<string, CustomPoint<double>> infoForTable , params SeriesDataInPoints<double>[] data )
        {
            chart1.Visible = false;
            chart2.Visible = false;
            chart3.Visible = false;
            chart4.Visible = false;
            chart5.Visible = false;
            this.chart1.ChartAreas[0].AxisX.Title = "Серії розрахунків";
            this.chart1.ChartAreas[0].AxisY.Title = "Значення";
            this.chart2.ChartAreas[0].AxisX.Title = "Серії розрахунків";
            this.chart2.ChartAreas[0].AxisY.Title = "Значення"; 
            this.chart3.ChartAreas[0].AxisX.Title = "Серії розрахунків";
            this.chart3.ChartAreas[0].AxisY.Title = "Значення"; 
            this.chart4.ChartAreas[0].AxisX.Title = "Серії розрахунків"; 
            this.chart4.ChartAreas[0].AxisY.Title = "Значення";
            this.chart5.ChartAreas[0].AxisX.Title = "Серії розрахунків"; 
            this.chart5.ChartAreas[0].AxisY.Title = "Значення"; 

            dataChart = data;
            titleChart = title;
            DrawChart<double>.Draw(ref chart1, type, title, data);
            DrawChart<double>.Draw(ref chart2, TypeOfCharts.Bar, title, data);
            DrawChart<double>.Draw(ref chart3, TypeOfCharts.Pie, title, data);
            DrawChart<double>.Draw(ref chart4, TypeOfCharts.Column, title, data);
            DrawChart<double>.Draw(ref chart5, TypeOfCharts.Area, title, data);
            dataGridView1.Columns.Add("first", "Серії розрахунків");
            dataGridView1.Columns.Add("second", "Значення");
            dataGridView1.Columns.Add("Info", "Інформація");
            List<DataGridViewRow> list = new List<DataGridViewRow>();
            foreach (var item in infoForTable)
            {
                dataGridView1.Rows.Add(item.Value.X, item.Value.Y, item.Key);
            }

            if (type == TypeOfCharts.Bar)  
                checkBox1.Checked = true;          
            if (type == TypeOfCharts.Pie)
                checkBox3.Checked = true;
            if (type == TypeOfCharts.Column)
                checkBox4.Checked = true;
            if (type == TypeOfCharts.Area)
                checkBox5.Checked = true;
            if (type == TypeOfCharts.Line)
            {
                chart1.Visible = true;
                checkBox2.Checked = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                chart1.Visible = false;
                chart3.Visible = false;
                chart4.Visible = false;
                chart5.Visible = false;
                chart2.Visible = true;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
                
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked)
            {
                chart1.Visible = true;
                chart2.Visible = false;
                chart3.Visible = false;
                chart4.Visible = false;
                chart5.Visible = false;
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
          
            }
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {

            if (checkBox3.Checked)
            {
                chart1.Visible = false;
                chart3.Visible = true;
                chart4.Visible = false;
                chart5.Visible = false;
                chart2.Visible = false;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
               
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox4.Checked)
            {
                chart1.Visible = false;
                chart3.Visible = false;
                chart4.Visible = true;
                chart5.Visible = false;
                chart2.Visible = false;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
             
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox5.Checked)
            {
                chart1.Visible = false;
                chart3.Visible = false;
                chart4.Visible = false;
                chart5.Visible = true;
                chart2.Visible = false;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
            else
            {
            
            }
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void chart5_Click(object sender, EventArgs e)
        {

        }
    }
}
