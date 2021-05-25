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
            var obj = db.GetRows("issues", "*", "");
            var issues = new List<Issue>();
            foreach (var row in obj)
            {
                issues.Add(IssueMapper.Map(row));
            }

            issuesLB.Items.AddRange(issues.ToArray());
        }
        private void clear_textbox()
        {
            nameTB.Text = "";
            descrTB.Text = "";
            TemaTB.Text = "";
            cmbBxTema.Text = ""; // add for UI
        }

        private void addBtn_Click(object sender, EventArgs e)
        {//insert
            if (nameTB.Text != "")
            {
                string[] fields = { "name", "description", "Tema" };
                string[] values = { DBUtil.AddQuotes(nameTB.Text), DBUtil.AddQuotes(descrTB.Text), 
                    DBUtil.AddQuotes(cmbBxTema.Text/*TemaTB.Text*/) }; // add for UI

                db.InsertToBD("issues", fields, values);
                RefreshIssues();
            } else
            {
                MessageBox.Show("Заповніть всі поля");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {//update
            if (nameTB.Text != "")
            {
                Issue item = issuesLB.SelectedItem as Issue;

                string[] cols = { "issue_id", "name", "description", "Tema" };
                string[] values = { item.Id.ToString(), DBUtil.AddQuotes(nameTB.Text),
                    DBUtil.AddQuotes(descrTB.Text), 
                    DBUtil.AddQuotes(cmbBxTema.Text/*TemaTB.Text*/) }; // add for UI
 

                 db.UpdateRecord("issues", cols, values);
                RefreshIssues();
            }
            else
            {
                MessageBox.Show("Заповніть всі поля");
            }
        }
        private void ShowIssue(Issue issue)
        {
            if (issue == null)
            {
                return;
            }

            nameTB.Text = issue.Name;
            descrTB.Text = issue.Description;
            TemaTB.Text = issue.Tema;
            cmbBxTema.Text= issue.Tema; //add for UI
        }
        private void issuesLB_SelectedIndexChanged(object sender, EventArgs e)
        {//show
            //Text = issuesLB.SelectedIndex.ToString();
            ShowIssue(issuesLB.SelectedItem as Issue);
        }
        
        private void bDelete_Click(object sender, EventArgs e)
        {//delete
            if (nameTB.Text != "")
            {
                var confirm = MessageBox.Show("Видалити задачу?", "Видалення", MessageBoxButtons.YesNo);
                Issue item = issuesLB.SelectedItem as Issue;
                if (confirm.Equals(DialogResult.Yes))
                {
                    db.DeleteFromDB("issues", "issue_id", item.Id.ToString());
                }
                RefreshIssues();
                clear_textbox();
            }
            else
            {
                MessageBox.Show("Оберіть задачу");
            }
        }

        private void nameTB_Click(object sender, EventArgs e)
		{
			new ToolTip().Show("Введіть назву задачі (до 100 символів)", (Control)sender, 0, ((Control)sender).Height, 2000);
		}

        private void descrTB_Click(object sender, EventArgs e)
        {
	        new ToolTip().Show("Введіть опис задачі (до 500 символів)", (Control)sender, 0, ((Control)sender).Height, 2000);
        }

        private void cmbBxTema_Click(object sender, EventArgs e)
        {
	        new ToolTip().Show("Введіть тему задачі (до 64 символів)", (Control)sender, 0, ((Control)sender).Height, 2000);
        }
    }
}