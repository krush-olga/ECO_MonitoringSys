using Data;
using Data.Entity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Drawing;
using HelpModule;

namespace oprForm
{
    public partial class PlannedEventsForm : Form
    {
        private DBManager db = new DBManager();
        private int user;
        private int valueCol = 2;
        private int descCol = 1;

        private Event[] originalEvents;
        private Resource[] originalResource;


        public PlannedEventsForm(int userId)
        {
            InitializeComponent();
            user = userId;

            originalEvents = new Event[0];
            originalResource = new Resource[0];
        }


        /* Begin - Серая подсказка для TextBox, когда пустое TextBox.Text*/

        TextBox[] txtBxMas = new TextBox[4]; //= { txtBxTemplate, txtBxRes, evNameTB, descTB };
        string[] placeholderMas = { "Пошук по шаблону", "Пошук по ресурсам", "Введіть назву заходу", "Введіть опис заходу" };

        private void PlaceholderTxtBx(TextBox txtBxName, string placeholder)
        {
            txtBxName.ForeColor = SystemColors.GrayText;
            txtBxName.Text = placeholder;
            txtBxName.Leave += TxtBx_Leave;
            txtBxName.Enter += TxtBx_Enter;
        }

        private void TxtBx_Enter(object sender, EventArgs e)
        {
            // throw new NotImplementedException();

            TextBox txtBx = sender as TextBox;
            bool check = false;
            for (int i = 0; i < placeholderMas.Length; i++) if (txtBx.Text == placeholderMas[i]) check = true;
            if (check)
            {
                txtBx.Text = "";
                txtBx.ForeColor = SystemColors.WindowText;
                txtBx.Tag = 1;
            }
        }

        private void TxtBx_Leave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            TextBox txtBx = sender as TextBox;
            string placeholder = "";

            if (txtBx.Text.Length == 0)
            {
                for (int i = 0; i < txtBxMas.Length; i++) if (txtBx.Name == txtBxMas[i].Name) placeholder = placeholderMas[i];
                txtBx.Text = placeholder;
                txtBx.ForeColor = SystemColors.GrayText;
                txtBx.Tag = null;
            }

        }
        /* End - Серая подсказка для TextBox, когда пустое TextBox.Text*/

        private void PlannedEventsForm_Load(object sender, EventArgs e)
        {
            /* Begin - Серая подсказка для TextBox, когда пустое TextBox.Text*/
            txtBxMas[0] = txtBxTemplate;
            txtBxMas[1] = txtBxRes;
            txtBxMas[2] = evNameTB;
            txtBxMas[3] = descTB;
            for (int i = 0; i < txtBxMas.Length; i++) PlaceholderTxtBx(txtBxMas[i], placeholderMas[i]);

            /* End - Серая подсказка для TextBox, когда пустое TextBox.Text*/

            db.Connect();
            var obj = db.GetRows("event_template", "*", "");
            var events = new List<Event>();
            foreach (var row in obj)
            {
                events.Add(EventTemplateMapper.Map(row));
            }
            originalEvents = events.ToArray();

            eventsLB.Items.AddRange(originalEvents);

            issuesCB.Items.Clear();
            var iss = db.GetRows("issues", "*", "");
            var issues = new List<Issue>();
            foreach (var row in iss)
            {
                issues.Add(IssueMapper.Map(row));
            }

            issuesCB.Items.AddRange(issues.ToArray());
            issuesCB.SelectedIndex = 0;

            var res = db.GetRows("resource", "*", "");
            var resources = new List<Resource>();
            foreach (var row in res)
            {
                resources.Add(ResourceMapper.Map(row));
            }

            originalResource = resources.ToArray();

            resLB.Items.AddRange(originalResource);

            db.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (eventsLB.SelectedItem == null)
            {
                MessageBox.Show("Виберіть шаблон заходу.",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (issuesCB.SelectedItem == null)
            {
                MessageBox.Show("Відсутня задача. Виберіть задачу для заходу.",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (evNameTB.Tag == null || eventsLB.Text == string.Empty || !(evNameTB.Tag is int))
            {
                MessageBox.Show("Відсутня назва заходу. Введіть назву заходу!", 
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (user == 0)
            {
                MessageBox.Show("Адмін не може додавати нові заходи.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Event ev = eventsLB.SelectedItem as Event;
            Issue issue = (Issue)issuesCB.SelectedItem;

            try
            {
                db.Connect();
                string evName = DBUtil.AddQuotes(evNameTB.Text);
                string evDesc = descTB.Tag == null || !(descTB.Tag is int) ? "'Опис відсутній.'" : DBUtil.AddQuotes(descTB.Text);

                string[] evFields = new string[] { "name", "description", "id_of_user", "issue_id" };

                string issueId = issue.Id.ToString();
                string[] evValues = new string[] { evName, evDesc, user.ToString(), issueId };

                int evId = db.InsertToBD("event", evFields, evValues);

                foreach (DataGridViewRow row in eventListGrid.Rows)
                {
                    if (row.Cells[0].Value is Resource)
                    {
                        var res = row.Cells[0].Value as Resource;
                        string desc = "";
                        string value = "";
                        if (row.Cells[descCol].Value != null)
                            desc = DBUtil.AddQuotes(row.Cells[descCol].Value.ToString());
                        if (row.Cells[valueCol].Value != null)
                            value = row.Cells[valueCol].Value.ToString();

                        string[] fields = { "event_id", "resource_id", "value", "description" };
                        string[] values = { evId.ToString(), res.Id.ToString(), value, desc };

                        db.InsertToBD("event_resource", fields, values);
                    }
                }
                db.Disconnect();

                MessageBox.Show("Захід " + evNameTB.Text + " додано.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void eventsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO Confirmation if data entered
            if (eventsLB.SelectedItem is Event)
            {
                addGB.Visible = true;
                db.Connect();
                Event ev = eventsLB.SelectedItem as Event;
                if (ev == null)
                {
                    db.Disconnect();
                    return;
                }
                var resourcesForEvent = db.GetRows("template_resource", "template_id, resource_id",
                    "template_id=" + ev.Id);
                var resources = new List<Resource>();
                foreach (var resForEvent in resourcesForEvent)
                {
                    var res = db.GetRows("resource", "*", "resource_id=" + resForEvent[1]);
                    resources.Add(ResourceMapper.Map(res[0]));
                }

                eventListGrid.Rows.Clear();
                foreach (var r in resources)
                {
                    eventListGrid.Rows.Add(r, r.Description);
                }
                descTB.Text = ev.Description;
                descTB.ForeColor = SystemColors.WindowText; //add UI

                db.Disconnect();
            }
        }

        // Assign value from data grid view to needed resource object
        private void commitValue(object sender, DataGridViewCellEventArgs e)
        {
            if (eventListGrid.Rows[e.RowIndex].Cells[0].Value is Resource)
            {
                var res = eventListGrid.Rows[e.RowIndex].Cells[0].Value as Resource;
                if (e.ColumnIndex == valueCol)
                {
                    try
                    {
                        var val = Int32.Parse(eventListGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        if (val > 0)
                        {
                            res.Value = val;
                            return;
                        }
                        else
                            throw new ArgumentException();
                    }
                    catch (FormatException)
                    {
                        cancelEdit();
                        MessageBox.Show("Введiть цiле число.");
                        return;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Число повинно бути > 0");
                    }
                }
                if (e.ColumnIndex == descCol)
                {
                    var val = eventListGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    if (val.Length != 0 && !val.Equals(" "))
                        res.Description = val;
                    else
                    {
                        MessageBox.Show("Опис не повинен бути пустим.");
                    }
                }
            }
        }

        private void cancelEdit()
        {
            //(eventListGrid.DataSource as DataTable).RejectChanges();
            eventListGrid.CancelEdit();
            eventListGrid.RefreshEdit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (onlyExpCB.Checked)
            {
                eventsLB.Items.Clear();
                db.Connect();
                var obj = db.GetRows("event_template", "*", "expert_id=" + user);
                var events = new List<Event>();
                foreach (var row in obj)
                {
                    events.Add(EventTemplateMapper.Map(row));
                }
                originalEvents = events.ToArray();

                eventsLB.Items.AddRange(originalEvents);
                db.Disconnect();
            }
            else
            {
                eventsLB.Items.Clear();
                PlannedEventsForm_Load(this, e);
            }
        }

        private void resLB_DoubleClick(object sender, EventArgs e)
        {
            Resource res = resLB.SelectedItem as Resource;

            foreach (DataGridViewRow row in eventListGrid.Rows)
            {
                if (row.Cells[0].Value == res)
                    return;
            }
            eventListGrid.Rows.Add(res, res.Description);
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Tag.ToString())
                {
                    case "1":

                        var lowerTemplateText = txtBxTemplate.Tag == null || !(txtBxTemplate.Tag is int) ? string.Empty : txtBxTemplate.Text.ToLower();

                        eventsLB.Items.Clear();
                        eventsLB.Items.AddRange(originalEvents.Where(_event => _event.Name.ToLower().Contains(lowerTemplateText))
                                                              .ToArray());
                        break;
                    case "2":
                        var lowerResultText = txtBxRes.Tag == null || !(txtBxRes.Tag is int) ? string.Empty : txtBxRes.Text.ToLower();

                        resLB.Items.Clear();
                        resLB.Items.AddRange(originalResource.Where(resource => resource.Name.ToLower().Contains(lowerResultText))
                                                             .ToArray());
                        break;
                    default:
                        break;
                }
            }
        }

        private void startTutorial_Click(object sender, EventArgs e)
        {
            var frm = new HelpToolTipForm(delegate
            {
                new InteractiveToolTipCreator().CreateTips(new List<InteractiveToolTipModel>
            {
              new InteractiveToolTipModel
              {
                Control = txtBxTemplate,
                Text = "Для пошуку шаблонів необхідно вписати запит у дане поле та натиснути на кнопку \"🔍\""
              },
              new InteractiveToolTipModel
              {
                Control = eventsLB,
                Text = "У даному блоці знаходяться всі шаблони заходів, які відповідають пошуковому запиту. Щоб обрати шаблон необхідно двічі клікнути на необхідний захід."
              },
              new InteractiveToolTipModel
              {
                Control = txtBxRes,
                Text = "Для пошуку ресурсів необхідно вписати запит у дане поле та натиснути на кнопку \"🔍\""
              },
                    new InteractiveToolTipModel
              {
                Control = resLB,
                Text = "У даному блоці знаходяться всі ресурси, які відповідають пошуковому запиту. Щоб обрати ресурс необхідно двічі клікнути на нього."
                    },
                    new InteractiveToolTipModel
                    {
                      Control = eventListGrid,
                      Text = "У блоці \"Перелік ресурсів заходу\" можна перегланути усі додані ресурси."
                    },
                     new InteractiveToolTipModel
                    {
                      Control = addGB,
                      Text = "Блок \"Додання нового заходу\""
                    },
                    new InteractiveToolTipModel
              {
                Control = evNameTB,
                Text = "В даному полі необхідно ввести назву нового заходу"
              },
              new InteractiveToolTipModel
              {
                Control = descTB,
                Text = "Поле \"Опис заходу\" заповнюється автоматично але його можно редагувати"
              },
              new InteractiveToolTipModel
              {
                Control = issuesCB,
                Text = "Поле \"Задача\" необхідно заповнити обравши необхідну задачу з випадаючого списку"
                    },
              new InteractiveToolTipModel
              {
                Control = addBtn,
                Text = "Для того щоб додати новий захід необхідно натиснути на кнопку \"Додати захід\""
              }
            });
            }, delegate
            {
                Help.ShowHelp(this, Config.PathToHelp, HelpNavigator.Topic, "p3.html");
            });
            frm.ShowDialog();
        }

        private void startTutorial_MouseEnter(object sender, EventArgs e)
        {
	        startTutorial.Font = new Font(startTutorial.Font, FontStyle.Bold);
        }

        private void startTutorial_MouseLeave(object sender, EventArgs e)
        {
	        startTutorial.Font = new Font(startTutorial.Font, FontStyle.Regular);
        }
    }
}