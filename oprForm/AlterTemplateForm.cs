using Data;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace oprForm
{
    public partial class AlterTemplateForm : Form
    {
        private DBManager db = new DBManager();

        public AlterTemplateForm()
        {
            InitializeComponent();
            db.Connect();
            var obj = db.GetRows("event_template", "*", "");
            var events = new List<Event>();
            foreach (var row in obj)
            {
                events.Add(EventTemplateMapper.Map(row));
            }
            templatesLB.Items.AddRange(events.ToArray());

            var res = db.GetRows("resource", "*", "");
            var resources = new List<Resource>();
            foreach (var row in res)
            {
                resources.Add(ResourceMapper.Map(row));
            }
            resourcesLB.Items.AddRange(resources.ToArray());

            db.Disconnect();
        }

        private void resourcesLB_DoubleClick(object sender, EventArgs e)
        {
            Resource res = resourcesLB.SelectedItem as Resource;

            foreach (DataGridViewRow row in materialListGrid.Rows)
            {
                if ((row.Cells[0].Value as Resource).Id == res.Id)
                    return;
            }
            materialListGrid.Rows.Add(res, res.Description);
        }

        private void templatesLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO Confirmation if data entered
            if (templatesLB.SelectedItem is Event)
            {
                Event ev = templatesLB.SelectedItem as Event;
                addGB.Visible = true;
                db.Connect();
                var resourcesForEvent = db.GetRows("template_resource", "template_id, resource_id",
                    "template_id=" + ev.Id);
                var resources = new List<Resource>();
                foreach (var resForEvent in resourcesForEvent)
                {
                    var res = db.GetRows("resource", "*", "resource_id=" + resForEvent[1]);
                    resources.Add(ResourceMapper.Map(res[0]));
                }

                materialListGrid.Rows.Clear();
                foreach (var r in resources)
                {
                    materialListGrid.Rows.Add(r, r.Description);
                }
                descTB.Text = ev.Description;
                nameTB.Text = ev.Name;

                db.Disconnect();
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            var ev = templatesLB.SelectedItem as Event;
            db.Connect();
            string[] cols = { "template_id", "name", "description" };
            string[] values = { ev.Id.ToString(), DBUtil.AddQuotes(nameTB.Text), DBUtil.AddQuotes(descTB.Text) };

            ev.Name = nameTB.Text;
            ev.Description = descTB.Text;

            templatesLB.Refresh();

            db.UpdateRecord("event_template", cols, values);

            //Get all resources for thar template
            var ress = db.GetRows("template_resource", "resource_id", "template_id=" + ev.Id);

            //Update resources for event
            foreach (DataGridViewRow row in materialListGrid.Rows)
            {
                var res = row.Cells[0].Value as Resource;

                //Remove present resources to delete left ones
                ress.RemoveAll(o => Int32.Parse(o[0].ToString()) == res.Id);

                string[] resCols = { "template_id" };
                string[] resValues = { ev.Id + " AND resource_id=" + res.Id };
                if (db.GetRows("template_resource", "*", "template_id=" + ev.Id + " AND resource_id=" + res.Id).Count == 0)
                {
                    string[] resColsIns = { "template_id", "resource_id" };
                    string[] resValuesIns = { ev.Id.ToString(), res.Id.ToString() };
                    db.InsertToBDWithoutId("template_resource", resColsIns, resValuesIns);
                }
            }

            //Delete resources thar are not in grid view
            foreach (var resId in ress)
            {
                string resCols = "template_id";
                string resValues = ev.Id + " AND resource_id=" + resId[0].ToString();

                db.DeleteFromDB("template_resource", resCols, resValues);
            }

            db.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (templatesLB.SelectedItem is Event)
            {
                Event ev = templatesLB.SelectedItem as Event;
                db.Connect();

                var ress = db.GetRows("template_resource", "resource_id", "template_id=" + ev.Id);
                foreach (var resId in ress)
                {
                    string resCols = "template_id";
                    string resValues = ev.Id + " AND resource_id=" + resId[0].ToString();

                    db.DeleteFromDB("template_resource", resCols, resValues);
                }

                db.DeleteFromDB("event_template", "template_id", ev.Id.ToString());
                db.Disconnect();

                templatesLB.Items.Remove(ev);
            }
        }

        private void materialListGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AlterTemplateForm_Load(object sender, EventArgs e)
        {

        }
    }
}