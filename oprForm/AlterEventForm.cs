﻿using Data;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Drawing;
using System.Linq;
using HelpModule;

namespace oprForm
{
    public partial class AlterEventForm : Form
    {
        private DBManager db = new DBManager();
        private int valueCol = 2;
        private int descCol = 1;

        private Event[] originalEvents;
        private Resource[] originalResource;

        /* Begin - Серая подсказка для TextBox, когда пустое TextBox.Text*/

        TextBox[] txtBxMas = new TextBox[2]; //= { txtBxTemplate, txtBxRes, evNameTB, descTB };
        string[] placeholderMas = { "Пошук по заходам", "Пошук по ресурсам"};

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

        public AlterEventForm()
        {
            InitializeComponent();
            db.Connect();

            /* Begin - Серая подсказка для TextBox, когда пустое TextBox.Text*/
            txtBxMas[0] = txtBxTemplate;
            txtBxMas[1] = txtBxRes;
            for (int i = 0; i < txtBxMas.Length; i++) PlaceholderTxtBx(txtBxMas[i], placeholderMas[i]);

            /* End - Серая подсказка для TextBox, когда пустое TextBox.Text*/

            getEvents();

            issuesCB.Items.Clear();
            var iss = db.GetRows("issues", "*", "");
            var issues = new List<Issue>();
            foreach (var row in iss)
            {
                issues.Add(IssueMapper.Map(row));
            }

            var _issues = issues.ToArray();
            issuesCB.Items.AddRange(_issues);
            findIssueCB.Items.Add(new Issue { Id = -1, Name = "Оберіть задачу" });
            findIssueCB.Items.AddRange(_issues);
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

        private void getEvents()
        {
            eventsLB.Items.Clear();
            var obj = db.GetRows("event", "*", "");
            var events = new List<Event>();
            foreach (var row in obj)
            {
                events.Add(EventMapper.Map(row));
            }
            originalEvents = events.ToArray();

            eventsLB.Items.AddRange(originalEvents);
        }

        private void eventsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO Confirmation if data entered
            if (eventsLB.SelectedItem is Event)
            {
                if (eventsLB.SelectedItem is Event)
                {
                    eventsLB.Focus();

                    Event ev = eventsLB.SelectedItem as Event;
                    alterGB.Visible = true;
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
                    foreach (var r in resources)
                    {
                        eventListGrid.Rows.Add(r, r.Description, r.Value);
                    }

                    updateEvent(ev);

                    db.Disconnect();
                }
            }
        }

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
            //TODO
        }

        private void updateEvent(Event ev)
        {
            evNameTB.Text = ev.Name;
            descTB.Text = ev.Description;

            foreach (var i in issuesCB.Items)
            {
                if (i is Issue)
                {
                    var iss = i as Issue;
                    if (iss.Id == ev.IssueId)
                    {
                        issuesCB.SelectedItem = iss;
                    }
                }
            }
        }

        private void SetEventLBFiltration()
        {
            var lowerTemplateText = txtBxTemplate.ForeColor == SystemColors.GrayText ? string.Empty : txtBxTemplate.Text.ToLower();
            var findIssue = findIssueCB.SelectedItem as Issue;

            eventsLB.Items.Clear();
            eventsLB.Items.AddRange(originalEvents.Where(_event => _event.Name.ToLower().Contains(lowerTemplateText) && 
                                                                   (findIssue?.Id == - 1 || _event.IssueId == findIssue?.Id))
                                                  .ToArray());
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Видалити захiд?", "Видалення", MessageBoxButtons.YesNo);

            if (confirm.Equals(DialogResult.Yes) && eventsLB.SelectedItem is Event)
            {
                Event ev = eventsLB.SelectedItem as Event;
                db.Connect();
                db.DeleteFromDB("event", "event_id", ev.Id.ToString());
                getEvents();
                db.Disconnect();
                eventListGrid.Rows.Clear();

                MessageBox.Show("Захід успішно видалений.","Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (evNameTB.Text == string.Empty)
            {
                MessageBox.Show("Відсутня назва заходу. Введіть назву заходу!",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var ev = eventsLB.SelectedItem as Event;
            var desc = descTB.Tag == null || !(descTB.Tag is int) ? "'Опис відсутній.'" : DBUtil.AddQuotes(descTB.Text);
            db.Connect();
            string[] cols = { "event_id", "name", "description", "issue_id" };
            string[] values = { ev.Id.ToString(), DBUtil.AddQuotes(evNameTB.Text), desc, (issuesCB.SelectedItem as Issue).Id.ToString() };

            db.UpdateRecord("event", cols, values);

            //Get all resources for thar event
            var ress = db.GetRows("event_resource", "resource_id", "event_id=" + ev.Id);

            //Update resources for event
            foreach (DataGridViewRow row in eventListGrid.Rows)
            {
                var res = row.Cells[0].Value as Resource;

                //Remove present resources to delete left ones
                ress.RemoveAll(o => Int32.Parse(o[0].ToString()) == res.Id);

                string[] resCols = { "event_id", "value", "description" };
                string[] resValues = { ev.Id + " AND resource_id=" + res.Id, res.Value.ToString(), DBUtil.AddQuotes(res.Description) };
                if (db.UpdateRecord("event_resource", resCols, resValues) == 0)
                {
                    string[] resColsIns = { "event_id", "resource_id", "value", "description" };
                    string[] resValuesIns = { ev.Id.ToString(), res.Id.ToString(), res.Value.ToString(), DBUtil.AddQuotes(res.Description) };
                    db.InsertToBDWithoutId("event_resource", resColsIns, resValuesIns);
                }
            }

            //Delete resources thar are not in grid view
            foreach (var resId in ress)
            {
                string resCols = "event_id";
                string resValues = ev.Id + " AND resource_id=" + resId[0].ToString();

                db.DeleteFromDB("event_resource", resCols, resValues);
            }

            getEvents();

            db.Disconnect();

            MessageBox.Show("Захід успішно змінений.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void addMaterial(object sender, EventArgs e)
        {
            Resource res = resLB.SelectedItem as Resource;

            foreach (DataGridViewRow row in eventListGrid.Rows)
            {
                if (row.Cells[0].Value.Equals(res))
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
                        SetEventLBFiltration();
                        break;
                    case "2":
                        var lowerResultText = txtBxRes.ForeColor == SystemColors.GrayText ? string.Empty : txtBxRes.Text.ToLower();

                        resLB.Items.Clear();
                        resLB.Items.AddRange(originalResource.Where(resource => resource.Name.ToLower().Contains(lowerResultText))
                                                             .ToArray());
                        break;
                    default:
                        break;
                }
            }
        }

        private void findIssueCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEventLBFiltration();
        }

        private void startTutorial_Click(object sender, EventArgs e)
        {
            var frm = new HelpToolTipForm(delegate
            {
                new InteractiveToolTipCreator().CreateTips(new List<InteractiveToolTipModel>
                {
                    new InteractiveToolTipModel
                    {
                        Control = label1,
                        Text = "Блок \"Список заходів\""
                    },
                    new InteractiveToolTipModel
                    {
                        Control = txtBxTemplate,
                        Text = "Для пошуку заходів необхідно вписати запит у дане поле та натиснути на кнопку \"🔍\""
                    },
                    new InteractiveToolTipModel
                    {
                        Control = findIssueCB,
                        Text = "Даний випадаючий список відповідає за фільтрацію заходів по задачі"
                    },
                    new InteractiveToolTipModel
                    {
                        Control = eventsLB,
                        Text = "У даному блоці знаходиться всі відфальтровані заходи"
                    },
                    new InteractiveToolTipModel
                    {
                        Control = label2,
                        Text = "Блок \"Ресурси\""
                    },
                    new InteractiveToolTipModel
                    {
                        Control = txtBxRes,
                        Text = "Для пошуку ресурсів необхідно вписати запит у дане поле та натиснути на кнопку \"🔍\""
                    },
           
                    new InteractiveToolTipModel
                    {
                        Control = resLB,
                        Text = "У даному блоці знаходиться всі ресурси, які відповідають пошуковому запитую  Щоб додати ресурс до певного заходу необхідно двічі клікнути на нf ymjuj."
                    },
                    new InteractiveToolTipModel
                    {
                        Control = eventListGrid,
                        Text = "У блоці \"Перелік ресурсів заходу\" можна перегланути усі додані ресурси."
                    },
                    new InteractiveToolTipModel
                    {
                        Control = eventsLB,
                        Text = "Щоб продовжити далі оберіть захід та ресурси",
                        IsNotFinal = true,
                        AfterHandler = AfterAlterGBShow
                    }
                });
            }, delegate
            {
                Help.ShowHelp(this, Config.PathToHelp, HelpNavigator.Topic, "p2.html");
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

        private void AfterAlterGBShow()
        {
            if (!alterGB.Visible)
            {
                eventsLB.SelectedIndexChanged += CheckEventsLBSelected;
            }
            else
            {
                ContinueTutorial();
            }
        }

        private void CheckEventsLBSelected(object sender, EventArgs e)
        {
            if (eventsLB.SelectedIndex != -1)
            {
                eventsLB.SelectedIndexChanged -= CheckEventsLBSelected;
                ContinueTutorial();
            }
        }

        private void ContinueTutorial()
        {
            new InteractiveToolTipCreator().CreateTips(new List<InteractiveToolTipModel>
            {
                new InteractiveToolTipModel
                {
                    Control = alterGB,
                    Text = "В цьому блоці можно переглянути та відредагувати назву, опис заходу та задачу"
                },
                
                new InteractiveToolTipModel
                {
                    Control = addBtn,
                    Text = "Щоб зберегти зміни необхідно натиснути на кнопку \"Зберегти зміни\""
                },
                new InteractiveToolTipModel
                {
                    Control = delBtn,
                    Text = "Щоб видалити захід необхідно натиснути на кнопку \"Видалити захід\""
                }
            });
        }

    }
}