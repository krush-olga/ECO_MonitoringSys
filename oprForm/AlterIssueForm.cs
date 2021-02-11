using Data;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace oprForm
{
    public partial class AlterIssueForm : Form
    {
        private DBManager db = new DBManager();
        private Issue item;

        public AlterIssueForm(Issue item)
        {
            if (item == null)
            {
                this.Close();
                this.DialogResult = DialogResult.Abort;
                return;
            }

            InitializeComponent();
            this.item = item;
            nameTB.Text = item.Name;
            descrTB.Text = item.Description;
            // Add choice to add null to calculation series
            var emptySeries = new CalculationSeries();
            emptySeries.Id = -1;
            emptySeries.Name = "Без серiї";

            //seriesCB.Items.Add(emptySeries);
            //seriesCB.SelectedIndex = 0; //Bugged

            // Add all series from db to combo box
            db.Connect();
            var obj = db.GetRows("calculations_description", "*", "");
            var calculations = new List<CalculationSeries>();
            foreach (var row in obj)
            {
                calculations.Add(CalculatoinSeriesMapper.Map(row));
            }

            //seriesCB.Items.AddRange(calculations.ToArray());
            db.Disconnect();

            //seriesCB.SelectedIndex = item.seriesId.Length == 0 ? 0 : Int32.Parse(item.seriesId); // TODO
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Видалити задачу?", "Видалення", MessageBoxButtons.YesNo);

            if (confirm.Equals(DialogResult.Yes))
            {
                db.Connect();
                db.DeleteFromDB("issues", "issue_id", item.Id.ToString());
                db.Disconnect();
                this.Close();
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            item.Name = nameTB.Text;
            item.Description = descrTB.Text;

            db.Connect();
            string[] cols = { "issue_id", "name", "description" };

            //int calcSeriesId = (seriesCB.SelectedItem as CalculationSeries).Id;
            //string calcSeries = calcSeriesId == -1 ? "null" : calcSeriesId.ToString();
            string[] values = { item.Id.ToString(), DBUtil.AddQuotes(nameTB.Text), DBUtil.AddQuotes(descrTB.Text) };

            db.UpdateRecord("issues", cols, values);
            db.Disconnect();
            this.Close();
        }
    }
}