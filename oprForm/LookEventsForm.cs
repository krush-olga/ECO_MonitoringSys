using Data;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LawFileBase;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace oprForm
{
    public partial class LookEventsForm : Form
    {
        private DBManager db;
        private AddIssueForm issueForm;
        private int issueCounter;
        private Role role;

        private List<Issue> issues;
        private Dictionary<Issue, Dictionary<Expert, List<Event>>> issuesInfos;
        private Dictionary<Issue, List<KeyValuePair<string, string>>> issuesDocs;
        private Dictionary<Event, List<KeyValuePair<string, string>>> eventsDocs;
        private Dictionary<int, string> expertsNames;

        public LookEventsForm(int expertId)
        {
            InitializeComponent();
            db = new DBManager();

            issues = new List<Issue>();
            expertsNames = new Dictionary<int, string>();
            issuesInfos = new Dictionary<Issue, Dictionary<Expert, List<Event>>>();
            issuesDocs = new Dictionary<Issue, List<KeyValuePair<string, string>>>();
            eventsDocs = new Dictionary<Event, List<KeyValuePair<string, string>>>();

            role = (Role)expertId;

            if (role != Role.Admin && role != Role.Analyst)
            {
                approveBtn.Visible = false;
                disaproveBtn.Visible = false;
            }

            UpdateIssues();
        }

        private Task<double> GetTotalIssueCost(IEnumerable<int> eventsId)
        {
            if (!eventsId.Any())
                return Task.FromResult(0.0);

            string tables = "event_resource, resource";
            string columns = "event_resource.value, resource.price";
            string joinConc = "event_resource.resource_id = resource.resource_id";
            System.Text.StringBuilder cond = new System.Text.StringBuilder();

            foreach (var eventId in eventsId)
            {
                cond.Append("event_resource.event_id = ");
                cond.Append(eventId);
                cond.Append(" OR ");
            }
            cond.Remove(cond.Length - 3, 3);

            return db.GetRowsUsingJoinAsync(tables, columns, joinConc, cond.ToString(), JoinType.LEFT)
                     .ContinueWith(result => result.Result.Sum(row => (int)row[0] * (double)row[1]));
        }
        private Task<KeyValuePair<List<KeyValuePair<int, Resource>>, double>> GetEventInfo(int eventId)
        {
            string tables = "event_resource, resource";
            string columns = "event_resource.value, resource.price, resource.resource_id, " +
                            "resource.name, resource.description, resource.units";
            string joinConc = "event_resource.resource_id = resource.resource_id";
            string cond = "event_resource.event_id = " + eventId.ToString();

            if (!db.ConnectionOpen)
                db.Connect();

            return db.GetRowsUsingJoinAsync(tables, columns, joinConc, cond, JoinType.LEFT)
                     .ContinueWith(result =>
                     {
                         if (result.Result.Count == 0)
                             return default;
                         else
                         {
                             var res = result.Result;

                             var resources = res.Select(row =>
                             {
                                 var resource = new Resource
                                 {
                                     Id = (int)row[2],
                                     Name = row[3].ToString(),
                                     Description = row[4].ToString(),
                                     Unit = row[5].ToString(),
                                     Price = (double)row[1]
                                 };

                                 return new KeyValuePair<int, Resource>((int)row[0], resource);
                             }).ToList();
                             var eventCost = res.Select(row => (int)row[0] * (double)row[1]).Sum();

                             return new KeyValuePair<List<KeyValuePair<int, Resource>>, double>(resources, eventCost);
                         }
                     });
        }

        private List<Event> GetExpertEvents(Expert expert)
        {
            List<Event> events = null;
            var issueInfo = issuesInfos[issues[issueCounter]];

            if (expert.Id != -1)
            {
                events = issueInfo.First(pair => pair.Key.Role == (Role)expert.Id).Value;
            }
            else
            {
                var allEvents = new List<Event>();

                foreach (var pair in issueInfo)
                {
                    allEvents.AddRange(pair.Value);
                }

                events = allEvents;
            }

            return events;
        }

        private async Task SetIssueInfo(Issue issue)
        {
            if (issue == null)
            {
                return;
            }

            if (!db.ConnectionOpen)
                db.Connect();

            SynchronizationContext.Current.Send(StartLoadingIssueInfo, null);

            if (issuesInfos.ContainsKey(issue) && issuesInfos[issue] != null)
            {
                var currentInfo = issuesInfos[issue];

                if (currentInfo == null)
                    return;

                SetExperts(currentInfo.Keys);

                try
                {
                    var allEventsIds = new List<int>();

                    foreach (var pair in currentInfo)
                    {
                        allEventsIds.AddRange(pair.Value.Select(_event => _event.Id));
                    }
                    var totalCost = await GetTotalIssueCost(allEventsIds);

                    SynchronizationContext.Current.Send(obj => issueCostTB.Text = obj + " грн", totalCost);
                }
                finally
                {
                }

                docsLB.DataSource = issuesDocs[issue];
                docsLB.DisplayMember = "Value";
            }
            else
            {
                issuesInfos[issue] = null;

                var currentIssueCounter = issueCounter;

                string tables = "event, user, expert";
                string columns = "event.event_id, event.name, event.description, event.id_of_user," +
                                 "user.id_of_user, user.id_of_expert, user.description, expert.expert_name," +
                                 "event.lawyer_vefirication, event.dm_verification";
                string joinCond = "event.id_of_user = user.id_of_user, user.id_of_expert = expert.id_of_expert";
                string cond = "event.issue_id = " + issue.Id.ToString();

                var eventTask = db.GetRowsUsingJoinAsync(tables, columns, joinCond, cond, JoinType.LEFT)
                                  .ContinueWith(result =>
                                          {
                                              return result.Result.Select(row =>
                                              {
                                                  var expertId = (int)row[5];
                                                  var _event = new Event
                                                  {
                                                      Id = (int)row[0],
                                                      Name = row[1].ToString(),
                                                      Description = row[2].ToString(),
                                                      UserId = (int)row[3],
                                                      IssueId = issue.Id,
                                                      LawyerVer = row[8] is DBNull ? null : row[8].ToString(),
                                                      DmVer = row[9] is DBNull ? null : row[9].ToString()
                                                  };
                                                  var user = new Expert
                                                  {
                                                      Id = (int)row[4],
                                                      Name = row[6].ToString(),
                                                      Role = (Role)expertId
                                                  };

                                                  expertsNames[expertId] = row[7].ToString();

                                                  return new KeyValuePair<Expert, Event>(user, _event);
                                              });
                                          })
                                  .ContinueWith(result =>
                                          {
                                              var res = result.Result;
                                              var uniqueUsers = res.GroupBy(item => item.Key.Id);
                                              var innerResult = uniqueUsers.GroupJoin(res,
                                                                              outer => outer.Key,
                                                                              inner => inner.Value.UserId,
                                                                              (outer, inner) =>
                                                                              {
                                                                                  var currentUser = outer.First().Key;
                                                                                  var events = new List<Event>();

                                                                                  foreach (var item in outer)
                                                                                      events.Add(item.Value);

                                                                                  return new KeyValuePair<Expert, List<Event>>(currentUser, events);
                                                                              })
                                                                            .ToDictionary(key => key.Key, value => value.Value);

                                              issuesInfos[issue] = innerResult;

                                              return innerResult;
                                          });

                var setTotalCostTask = eventTask.ContinueWith(result =>
                                                {
                                                    var allEventsIds = new List<int>();

                                                    foreach (var pair in result.Result)
                                                    {
                                                        allEventsIds.AddRange(pair.Value.Select(_event => _event.Id));
                                                    }

                                                    return GetTotalIssueCost(allEventsIds);
                                                })
                                                .Unwrap();

                var documentsTask = db.GetRowsAsync("issues_documents", "document_code", "issue_id = " + issue.Id.ToString())
                                      .ContinueWith(result =>
                                      {
                                          var sm = new SearchManager();

                                          var documentsNames = result.Result.Select(row => new KeyValuePair<string, string>(row[0].ToString(),
                                                                                                                        sm.GetPrewiew(row[0].ToString())))
                                                                            .ToList();

                                          issuesDocs[issue] = documentsNames;

                                          return documentsNames;
                                      });

                await Task.WhenAll(eventTask, documentsTask, setTotalCostTask)
                          .ContinueWith(result =>
                          {
                              if (setTotalCostTask.IsCompleted)
                                  issueCostTB.Text = setTotalCostTask.Result.ToString() + " грн";

                              if (currentIssueCounter == issueCounter)
                              {
                                  if (eventTask.IsCompleted)
                                      SetExperts(eventTask.Result.Keys);

                                  if (documentsTask.IsCompleted)
                                  {
                                      docsLB.DataSource = documentsTask.Result;
                                      docsLB.DisplayMember = "Value";
                                  }
                              }
                          }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            SynchronizationContext.Current.Post(EndLoadingIssueInfo, null);
        }
        private async Task SetEventDocs(Event @event)
        {
            if (eventsDocs.ContainsKey(@event) && eventsDocs[@event] != null)
            {
                docsLB.DataSource = eventsDocs[@event];
            }
            else
            {
                if (!db.ConnectionOpen)
                    db.Connect();

                eventsDocs[@event] = null;

                await db.GetRowsAsync("event_documents", "document_code", "event_id=" + @event.Id.ToString())
                        .ContinueWith(result =>
                        {
                            var sm = new SearchManager();

                            var documentsNames = result.Result.Select(row => new KeyValuePair<string, string>(row[0].ToString(),
                                                                              sm.GetPrewiew(row[0].ToString())))
                                                              .ToList();

                            eventsDocs[@event] = documentsNames;

                            return documentsNames;
                        })
                        .ContinueWith(result =>
                        {
                            if (result.Result.Count != 0)
                            {
                                docsLB.DataSource = result.Result.ToList();
                                docsLB.DisplayMember = "Value";
                            }
                            else
                                docsLB.DataSource = null;
                        }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void SetExperts(IEnumerable<Expert> users)
        {
            var experts = expertsNames.Join(users,
                                            outer => outer.Key,
                                            inner => (int)inner.Role,
                                            (outer, inner) => outer)
                                      .ToList();

            experts.Insert(0, new KeyValuePair<int, string>(-1, "Усі"));

            expertsLB.DataSource = experts;
            expertsLB.DisplayMember = "Value";

            EndLoadingIssueInfo(null);
        }
        private void ResetFilterButtons()
        {
            if (docsLB.DataSource != null)
                EventDocsFilterButton.Enabled = true;
            else
                EventDocsFilterButton.Enabled = false;

            IssueDocsFilterButton.Enabled = false;
        }

        private void UpdateIssues()
        {
            try
            {
                if (!db.ConnectionOpen)
                    db.Connect();

                var obj = db.GetRows("issues", "*", "");

                issues.Clear();
                eventsDocs.Clear();
                issuesDocs.Clear();
                eventsDocs.Clear();

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
        }
        private void UpdateEvents(Func<Event, bool> functor)
        {
            var _events = GetExpertEvents(new Expert { Id = ((KeyValuePair<int, string>)expertsLB.SelectedItem).Key });

            eventsLB.DataSource = _events.Where(functor).ToList();
        }

        private void UpdateApproveGroupBoxComponent()
        {
            if (issues.Count != 0)
            {
                issueTB.Text = issues[0].Name;
                issueDescTB.Text = issues[0].Description;
                TemaTextBox.Text = issues[0].Tema;
            }
            else
            {
                issueTB.Text = "";
                issueDescTB.Text = "";
                TemaTextBox.Text = "";
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

        private void StartLoadingIssueInfo(object state)
        {
            expertsLB.DataSource = null;
            eventsLB.DataSource = null;
            docsLB.DataSource = null;

            expertsLB.Enabled = false;
            eventsLB.Enabled = false;
            docsLB.Enabled = false;

            var str = "Завантаження...";
            issueCostTB.Text = str;
            expertsLB.Items.Add(str);
            eventsLB.Items.Add(str);

            if (docsLB.Items.Count == 0)
                docsLB.Items.Add(str);
        }
        private void EndLoadingIssueInfo(object state)
        {
            expertsLB.Enabled = true;
            eventsLB.Enabled = true;
            docsLB.Enabled = true;

            if (issueCostTB.Text == "Завантаження...")
                issueCostTB.Text = "";
        }
        private void SetEventInfo(object state)
        {
            var result = (KeyValuePair<List<KeyValuePair<int, Resource>>, double>)state;

            eventListGrid.Rows.Clear();

            if (result.Key == null)
            {
                EventCostTextBox.Text = "0 грн";
                return;
            }

            EventCostTextBox.Text = result.Value.ToString() + " грн";
            eventListGrid.Rows.Add(result.Key.Count);

            for (int i = 0; i < result.Key.Count; i++)
            {
                var currentRow = eventListGrid.Rows[i];
                var currentResource = result.Key[i].Value;
                var resourceAmount = result.Key[i].Key;

                currentRow.Cells[0].Value = currentResource.Name;
                currentRow.Cells[1].Value = currentResource.Description;
                currentRow.Cells[2].Value = resourceAmount;
                currentRow.Cells[3].Value = currentResource.Unit;
                currentRow.Cells[4].Value = currentResource.Price;
                currentRow.Cells[5].Value = currentResource.Price * resourceAmount + " грн";
            }
        }

        private void SetApproved(bool approved)
        {
            if (eventsLB.SelectedItem != null && eventsLB.SelectedItem is Event ev)
            {
                if (ev.DmVer != null)
                {
                    var approvedStr = approved ? "підтвердити" : "відхилити";
                    var currentApprovedStr = ev.DmVer == "1" ? "підверджений" : "відхилений";

                    MessageBox.Show($"Не можливо {approvedStr} захід так як він вже {currentApprovedStr}.", 
                                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    if (!db.ConnectionOpen)
                        db.Connect();

                    ev.DmVer = approved ? "1" : "0";

                    updateEvent(ev);

                    string[] cols = { "event_id", "dm_verification" };
                    string[] values = { ev.Id.ToString(), approved.ToString() };

                    db.UpdateRecord("event", cols, values);

                    MessageBox.Show($"Захід \"{ev.Name}\" було {(approved ? "підтверджено" : "відхилено")}", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CheckedOnclyDisChanged();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void LookEventsForm_Load(object sender, EventArgs e)
        {
            var issue = issues[issueCounter];

            await SetIssueInfo(issue);

            if (issuesDocs[issue].Count != 0)
            {
                IssueDocsFilterButton.Enabled = true;
                EventDocsFilterButton.Enabled = false;
            }
        }

        private async void eventsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventsLB.SelectedItem is Event _event)
            {
                if (!EventDocsFilterButton.Enabled)
                {
                    await SetEventDocs(_event);
                }
                else if (!IssueDocsFilterButton.Enabled)
                {
                    docsLB.DataSource = issuesDocs[issues[issueCounter]];
                    docsLB.DisplayMember = "Value";
                }

                KeyValuePair<List<KeyValuePair<int, Resource>>, double> eventCost = default;

                try
                {
                    eventCost = await GetEventInfo(_event.Id);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не владось завантажити информацию по заходу.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                var currentSycnContext = SynchronizationContext.Current;

                currentSycnContext.Post(obj =>
                {
                    var __event = obj as Event;

                    EventNameTextBox.Text = __event.Name;
                    EventDescTextBox.Text = __event.Description;

                    if (_event.DmVer != null)
                    {
                        approveBtn.Enabled = false;
                        disaproveBtn.Enabled = false;
                    }
                    else
                    {
                        approveBtn.Enabled = true;
                        disaproveBtn.Enabled = true;
                    }
                }, _event);
                currentSycnContext.Post(SetEventInfo, eventCost);
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
            return string.IsNullOrEmpty(str) ? "не переглянуто" : str.Equals("0") ? "нi" : "так";
        }


        private void approveBtn_Click(object sender, EventArgs e)
        {
            SetApproved(true);
        }

        private void disaproveBtn_Click(object sender, EventArgs e)
        {
            SetApproved(false);
        }

        private void CheckedOnclyDisChanged()
        {
            if (onlyDisCB.Checked)
                UpdateEvents(_event => _event.DmVer == null || _event.DmVer != "1");
            else
                UpdateEvents(_event => true);
        }

        private async void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckedOnclyDisChanged();
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
            if (expertsLB.SelectedItem is KeyValuePair<int, string> expertInfo)
            {
                eventsLB.DataSource = GetExpertEvents(new Expert { Id = expertInfo.Key });
            }
        }

        private async void issuesLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (issuesLB.SelectedItem == null)
            {
                return;
            }

            if (issuesLB.SelectedItem is Issue issue)
            {
                await SetIssueInfo(issue);
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
                var findIssues = issues.Where(issue => issue.Name.Contains(findIssueCondTB.Text) ||
                                                       issue.Description.Contains(findIssueCondTB.Text) ||
                                                       issue.Tema.Contains(findIssueCondTB.Text));

                if (!findIssues.Any())
                {
                    MessageBox.Show("Результат відсутній.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                issuesLB.Items.Clear();

                foreach (var issue in findIssues)
                {
                    issuesLB.Items.Add(issue);
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            OpenAddIssueForm();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            PlannedEventsForm child = new PlannedEventsForm((int)role);
            child.Show();
        }

        private async void NextIssueClick(object sender, EventArgs e)
        {
            if (issueCounter < issues.Count - 1)
            {
                issueCounter++;
            }
            else if (issueCounter == issues.Count - 1)
            {
                issueCounter = 0;
            }

            var currentIssue = issues[issueCounter];

            issueTB.Text = currentIssue.Name;
            issueDescTB.Text = currentIssue.Description;
            TemaTextBox.Text = currentIssue.Tema;

            await SetIssueInfo(currentIssue);

            ResetFilterButtons();
        }
        private async void PreviousIssueClick(object sender, EventArgs e)
        {
            if (issueCounter > 0)
            {
                issueCounter--;
            }
            else if (issueCounter == 0)
            {
                issueCounter = issues.Count - 1;
            }

            var currentIssue = issues[issueCounter];

            issueTB.Text = currentIssue.Name;
            issueDescTB.Text = currentIssue.Description;
            TemaTextBox.Text = currentIssue.Tema;

            await SetIssueInfo(currentIssue);

            ResetFilterButtons();
        }

        private async void EventDocsSortButton_Click(object sender, EventArgs e)
        {
            IssueDocsFilterButton.Enabled = true;
            EventDocsFilterButton.Enabled = false;

            if (eventsLB.SelectedItem != null)
                await SetEventDocs(eventsLB.SelectedItem as Event);
            else
                docsLB.DataSource = null;
        }
        private void IssueDocsSortButton_Click(object sender, EventArgs e)
        {
            IssueDocsFilterButton.Enabled = false;
            EventDocsFilterButton.Enabled = true;

            var issue = issues[issueCounter];

            if (issuesDocs.ContainsKey(issue))
            {
                docsLB.DataSource = issuesDocs[issue];
                docsLB.DisplayMember = "Value";
            }
            else
                docsLB.DataSource = null;
        }

        private static string GetPathToDefaultBrowser()
        {
            const string currentUserSubKey =
            @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice";
            using (RegistryKey userChoiceKey = Registry.CurrentUser.OpenSubKey(currentUserSubKey, false))
            {
                string progId = (userChoiceKey.GetValue("ProgId").ToString());
                using (RegistryKey kp =
                       Registry.ClassesRoot.OpenSubKey(progId + @"\shell\open\command", false))
                {
                    // Get default value and convert to EXE path.
                    // It's stored as:
                    //    "C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"
                    // So we want the first quoted string only
                    string rawValue = (string)kp.GetValue("");
                    Regex reg = new Regex("(?<=\").*?(?=\")");
                    Match m = reg.Match(rawValue);
                    return m.Success ? m.Value : "";
                }
            }
        }

        private void OpenInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (docsLB.SelectedItem == null)
                return;

            var item = (KeyValuePair<string, string>)docsLB.SelectedItem;

            var filePath = Path.Combine(System.Environment.CurrentDirectory, @"FB\" + item.Key);
            var fileExtension = string.Empty;

            if (File.Exists(filePath + ".html"))
            {
                fileExtension = ".html";
            }
            else if (File.Exists(filePath + ".htm"))
            {
                fileExtension = ".htm";
            }
            else
            {
                MessageBox.Show("Вибранний документ не знайдено на комп'ютері.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var browserPath = GetPathToDefaultBrowser();

            Process.Start(browserPath, "\"" + filePath + fileExtension + "\"");
        }

        private void docsLB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DocContextMenuStrip.Show(docsLB, e.Location);
        }
    }
}
