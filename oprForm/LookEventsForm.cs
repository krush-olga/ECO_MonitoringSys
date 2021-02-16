using Data;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace oprForm
{
    public partial class LookEventsForm : Form
    {
        private DBManager db = new DBManager();
        private AddIssueForm issueForm;
        private int issueCounter;
        private int userId;

        private List<Event> events;
        private List<Issue> issues;
        private Dictionary<int, int> expertOfUser;

        public LookEventsForm(int userId)
        {
            InitializeComponent();
            db = new DBManager();

            events = new List<Event>();
            issues = new List<Issue>();
            expertOfUser = new Dictionary<int, int>();

            this.userId = userId;

            UpdateIssues();
        }

        private void UpdateIssues()
        {
            try
            {
                db.Connect();
                var obj = db.GetRows("issues", "*", "");
                issues.Clear();
                foreach (var row in obj)
                {
                    issues.Add(IssueMapper.Map(row));
                }
                UpdateApproveGroupBoxComponent();
                issueCounter = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.Disconnect();
            }
        }

        private void UpdateEvents()
        {
            try
            {
                eventsLB.Items.Clear();

                db.Connect();

                var obj = db.GetRows("event", "*", "dm_verification is NULL");
                var events = new List<Event>();

                foreach (var row in obj)
                {
                    events.Add(EventMapper.Map(row));
                }

                eventsLB.Items.AddRange(events.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.Disconnect();
            }
        }

        private void UpdateApproveGroupBoxComponent()
        {
            if (issues.Count != 0)
            {
                issueTB.Text = issues[0].Name;
                issueDescTB.Text = issues[0].Description;
                textBox2.Text = issues[0].Tema;
            }
            else
            {
                issueTB.Text = "";
                issueDescTB.Text = "";
                textBox2.Text = "";
            }

            if (issues.Count > 1)
            {
                nextIssueBtn.Enabled = true;
                previousBtn.Enabled = true;
            }
            else
            {
                nextIssueBtn.Enabled = false;
                previousBtn.Enabled = false;
            }
        }

        private void OpenAddIssueForm()
        {
            if (issueForm == null)
            {
                issueForm = new AddIssueForm();
                issueForm.FormClosed += (_sender, _event) =>
                {
                    issueForm.Dispose();
                    issueForm = null;

                    UpdateIssues();
                };

                issueForm.Show();
            }
            issueForm.BringToFront();
        }

        private void SetApproved(bool approved)
        {
            if (eventsLB.SelectedItem != null && eventsLB.SelectedItem is Event ev)
            {
                try
                {
                    db.Connect();

                    ev.DmVer = approved ? "1" : "0";

                    updateEvent(ev);

                    string[] cols = { "event_id", "dm_verification" };
                    string[] values = { ev.Id.ToString(), approved.ToString() };

                    db.UpdateRecord("event", cols, values);

                    MessageBox.Show($"Захід \"{ev.Name}\" було {(approved ? "підтверджено" : "відхилено")}", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    UpdateEvents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.Disconnect();
                }
            }
        }

        private void eventsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventsLB.SelectedItem is Event)
            {
            
                Event ev = eventsLB.SelectedItem as Event;
                approveGB.Visible = true;
                db.Connect();
                var resourcesForEvent = db.GetRows("event_resource", "event_id, resource_id, description, value",
                    "event_id=" + ev.Id);
                var resources = new List<Resource>();
                foreach (var resForEvent in resourcesForEvent)
                {
                    var res = db.GetRows("resource", "*", "resource_id=" + resForEvent[1]);
                    var resource = ResourceMapper.Map(res[0]);
                    resource.Description = resForEvent[2].ToString();
                    resource.Value = Int32.Parse(resForEvent[3].ToString());
                    resources.Add(resource);
                }

                eventListGrid.Rows.Clear();
                double totalPrice = 0;
                foreach (var r in resources)
                {
                    eventListGrid.Rows.Add(r, r.Description, r.Value, r.Unit, r.Price, r.Value * r.Price);
                    totalPrice += r.Value * r.Price;
                }
                textBox6.Text = totalPrice.ToString();

                updateEvent(ev);

                textBox5.Text = ev.Name;
                textBox4.Text = ev.Description;

                var docObj = db.GetRows("event_documents", "*", "event_id=" + ev.Id);
                var docs = new List<Document>();
                foreach (var row in docObj)
                {
                    docs.Add(DocumentMapper.Map(row));
                }
                docsLB.Items.Clear();
                docsLB.Items.AddRange(docs.ToArray());

                db.Disconnect();
            }
        }

        private bool ukrToBool(string str)
        {
            if (ukrBool(str) == "ні")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void updateEvent(Event ev)
        {
            
            dmCheck.Checked = ukrToBool(ev.DmVer);
            lawyerCheck.Checked = ukrToBool(ev.LawyerVer);

            if (ev.IssueId != -1)
            {
                var issName = db.GetValue("issues", "name", "issue_id=" + ev.IssueId);
                issueTB.Text = issName.ToString();
            }
            else
            {
                issueTB.Text = "None";
            }
        }

        private string ukrBool(string str)
        {
            return str == "" ? "не переглянуто" : str.Equals("0") ? "нi" : "так";
        }


        private void approveBtn_Click(object sender, EventArgs e)
        {
            SetApproved(true);
        }

        private void disaproveBtn_Click(object sender, EventArgs e)
        {
            SetApproved(false);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (onlyDisCB.Checked)
            {
                UpdateEvents();
            }
            else
            {
                eventsLB.Items.Clear();
                UpdateApproveGroupBoxComponent();
            }
        }

        private void docsLB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (docsLB.SelectedItem is Document)
            {
                var doc = docsLB.SelectedItem as Document;
                var child = new DocumentViewForm(doc.document_code);
                child.ShowDialog(this);
            }
        }

        private void expertsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (expertsLB.SelectedItem is Expert)
            {
                var expert = expertsLB.SelectedItem as Expert;
                eventsLB.Items.Clear();
                if (expert.Id == -1)
                {
                    eventsLB.Items.AddRange(events.ToArray());
                }
                else
                {
                    eventsLB.Items.AddRange((from ev in events where expertOfUser[ev.UserId] == expert.Id select ev).ToArray());
                }
            }
        }

        private double GetTotalCost(int eventId)
        {
            //db.Connect();
            var resourcesForEvent = db.GetRows("event_resource", "event_id, resource_id, description, value",
                "event_id=" + eventId);
            var resources = new List<Resource>();
            foreach (var resForEvent in resourcesForEvent)
            {
                var res = db.GetRows("resource", "*", "resource_id=" + resForEvent[1]);
                var resource = ResourceMapper.Map(res[0]);
                resource.Description = resForEvent[2].ToString();
                resource.Value = Int32.Parse(resForEvent[3].ToString());
                resources.Add(resource);
            }

            double totalPrice = 0;
            foreach (var r in resources)
            {
                totalPrice += r.Value * r.Price;
            }

            return totalPrice;
        }

        private void issuesLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (issuesLB.SelectedItem == null)
            {
                return;
            }

            if (issuesLB.SelectedItem is Issue)
            {
                eventsLB.Items.Clear();
                db.Connect();
                var obj = db.GetRows("event", "*", "issue_id=" + (issuesLB.SelectedItem as Issue).Id);
                if (obj.Count != 0)
                {
                    events.Clear();
                    foreach (var row in obj)
                    {
                        events.Add(EventMapper.Map(row));
                    }

                    double issueCost = 0;
                    foreach (var ev in events)
                    {
                        issueCost += GetTotalCost(ev.Id);
                    }
                    issueCostTB.Text = issueCost.ToString();
                    textBox6.Text = issueCost.ToString();

                    // Get list of user ids
                    var userIds = (from ev in events select Int32.Parse(ev.UserId.ToString())).ToArray();

                    // Get list of expert ids from users
                    List<int> expertIds = new List<int>();
                    foreach (var uid in userIds)
                    {
                        expertIds.Add(Int32.Parse(db.GetValue("user", "id_of_expert", "id_of_user=" + uid).ToString()));
                    }
                    obj = db.GetRows("expert", "distinct id_of_expert, expert_name", "id_of_expert in (" + String.Join(",", expertIds) + ")");

                    for (int i = 0; i < userIds.Length; i++)
                    {
                        expertOfUser[userIds[i]] = expertIds[i];
                    }

                    var experts = new List<Expert>();

                    var emptyExpert = new Expert();
                    emptyExpert.Id = -1;
                    emptyExpert.Name = "Усі";
                    experts.Add(emptyExpert);

                    expertsLB.Items.Clear();
                    foreach (var row in obj)
                    {
                        experts.Add(ExpertMapper.Map(row));
                    }
                    expertsLB.Items.AddRange(experts.ToArray());
                }
                var issue = db.GetRows("issues", "name, description, Tema", "issue_id=" + (issuesLB.SelectedItem as Issue).Id);
                if (issue.Count > 0)
                {
                    issueTB.Text = issue[0][0].ToString();
                    issueDescTB.Text = issue[0][1].ToString();
                    textBox2.Text = issue[0][2].ToString();
                    textBox4.Text = issue[0][0].ToString();
                    textBox5.Text = issue[0][1].ToString();
                }

              //  db.Disconnect();
            }

            
        }

        private void IssueListClick(object sender, EventArgs e)
        { 
            OpenAddIssueForm();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (findIssueCondTB.Text == "")
            {
                MessageBox.Show("Поле пошуку пусте.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    db.Connect();

                    var findIssues = db.GetRows("issues", "*",
                                                " name LIKE '%" + findIssueCondTB.Text +
                                                "%' OR description LIKE '%" + findIssueCondTB.Text +
                                                "%' OR Tema LIKE '%" + findIssueCondTB.Text + "%'");

                    if (findIssues.Count == 0)
                    {
                        MessageBox.Show("Результат відсутній.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    issuesLB.Items.Clear();

                    foreach (var row in findIssues)
                    {
                        issuesLB.Items.Add(IssueMapper.Map(row));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.Disconnect();
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            OpenAddIssueForm();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            PlannedEventsForm child = new PlannedEventsForm(userId);
            child.Show();
        }

        private void NextIssueClick(object sender, EventArgs e)
        {
            if (issueCounter < issues.Count - 1)
            {
                issueCounter++;

                issueTB.Text = issues[issueCounter].Name;
                issueDescTB.Text = issues[issueCounter].Description;
                textBox2.Text = issues[issueCounter].Tema;
            }
            else if (issueCounter == issues.Count - 1)
            {
                issueCounter = 0;

                issueTB.Text = issues[issueCounter].Name;
                issueDescTB.Text = issues[issueCounter].Description;
                textBox2.Text = issues[issueCounter].Tema;
            }
        }

        private void PreviousIssueClick(object sender, EventArgs e)
        {
            if (issueCounter > 0)
            {
                issueCounter--;

                issueTB.Text = issues[issueCounter].Name;
                issueDescTB.Text = issues[issueCounter].Description;
                textBox2.Text = issues[issueCounter].Tema;
            }
            else if (issueCounter == 0)
            {
                issueCounter = issues.Count - 1;

                issueTB.Text = issues[issueCounter].Name;
                issueDescTB.Text = issues[issueCounter].Description;
                textBox2.Text = issues[issueCounter].Tema;
            }
        }
    }
}