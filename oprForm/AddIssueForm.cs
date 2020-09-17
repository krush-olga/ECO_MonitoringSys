using Data;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Data.Entity;

namespace oprForm
{
    public partial class AddIssueForm : Form
    {
        private DBManager db = new DBManager();

        public AddIssueForm()
        {
            InitializeComponent();
            RefreshIssues();
        }

        private void RefreshIssues()
        {
            issuesLB.Items.Clear();
            db.Connect();
            var obj = db.GetRows("issues", "*", "");
            var issues = new List<Issue>();
            foreach (var row in obj)
            {
                issues.Add(IssueMapper.Map(row));
            }

            issuesLB.Items.AddRange(issues.ToArray());
            db.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTB.Text))
            {
                MessageBox.Show("Назва не може бути порожньою.");
                return;
            }

            db.Connect();
            string[] fields = { "name", "description", "Tema" };

            // Менять кавычки
            string[] values = { DBUtil.AddQuotes(nameTB.Text), DBUtil.AddQuotes(descrTB.Text) , DBUtil.AddQuotes(TemaTB.Text)};

            int id = db.InsertToBD("issues", fields, values);

            //Issue issue = new Issue(id, nameTB.Text, descrTB.Text, DateTime.Now, calcSeries);
            //issuesLB.Items.Add(issue);

            db.Disconnect();

            RefreshIssues();
        }

        private void ShowIssue(Issue issue)
        {
            if (issue == null)
            {
                return;
            }

            nameTB.Text = issue.name;
            descrTB.Text = issue.description;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var form = new AlterIssueForm(issuesLB.SelectedItem as Issue);

            if (form.DialogResult != DialogResult.Abort)
            {
                form.ShowDialog(this);
                ShowIssue(issuesLB.SelectedItem as Issue);
                RefreshIssues();
            }
        }

        private void issuesLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowIssue(issuesLB.SelectedItem as Issue);
        }
    }
}