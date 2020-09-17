using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using Data.Entity;

namespace oprForm
{
    public partial class IssuesForm : Form
    {
		private DBManager db = new DBManager();
		private String user = "Vasya";

        public IssuesForm()
        {
            InitializeComponent();
 			db.Connect();
			var obj = db.GetRows("issues", "*", "");
			var issues = new List<Issue>();
			foreach(var row in obj)
			{
				issues.Add(IssueMapper.Map(row));
			}
				

			issuesLB.Items.AddRange(issues.ToArray());
			db.Disconnect();
       }

        private void issuesLB_SelectedIndexChanged(object sender, EventArgs e)
        {
			Issue issue = issuesLB.SelectedItem as Issue;
            nameLbl.Text = issue.name;
            descrLbl.Text = issue.description;
            dateLbl.Text = issue.creationDate.ToString();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            db.Connect();
            string[] fields = { "name", "description", "calc_series_id" };
            
            string calcSeries = seriesTB.Text.Length != 0 ? seriesTB.Text : "null";
            string[] values = { DBUtil.AddQuotes(nameTB.Text), DBUtil.AddQuotes(descrTB.Text),  calcSeries};

            int id = db.InsertToBD("issues", fields, values);

            Issue issue = new Issue(id, nameTB.Text, descrTB.Text, DateTime.Now, calcSeries);
            issuesLB.Items.Add(issue);

            db.Disconnect();
        }
    }
}
