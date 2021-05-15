using Data;
using Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using HelpModule;

namespace oprForm
{
    public partial class MaterialsForm : Form
    {
        private DBManager db = new DBManager();
        private int nameColIdx = 0;
        private int priceColIdx = 1;
        private int unitsColIdx = 2;
        private int descColIdx = 3;
        private Resource saved;
        int cur_row = 0;

        public MaterialsForm()
        {
            InitializeComponent();

            getMaterials();
        }

        private void getMaterials()
        {
            resDGV.Rows.Clear();
            db.Connect();
            var obj = db.GetRows("resource", "*", "");
            var res = new List<Resource>();
            foreach (var row in obj)
            {
                res.Add(ResourceMapper.Map(row));
            }

            foreach (var r in res)
            {
                resDGV.Rows.Add(r, r.Price, r.Unit, r.Description);
            }

            db.Disconnect();
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            var selected = resDGV.SelectedRows;
            if (selected.Count == 0)
            {
                MessageBox.Show("Виділіть рядок або рядки, які бажаєте видалити. \n\n" +
                                "Для цього натисніть на клітинку курсору (пусту клітинку зліва)\n" +
                                "бажаного рядка або виділіть декілька рядків");
            }
            else
            {
                foreach (DataGridViewRow row in selected)
                {
                    if (row.Cells[0].Value is Resource)
                    {
                        var res = row.Cells[0].Value as Resource;
                        try
                        {
                            db.Connect();
                            string cols = "resource_id";
                            string values = res.Id.ToString();

                            db.DeleteFromDB("resource", cols, values);
                            resDGV.Rows.Remove(row);
                        }
                        catch (MySqlException ex)
                        {
                            if (ex.Number == 1451)
                            {
                                MessageBox.Show("Ресурс використовується");
                            }
                        }
                        finally
                        {
                            db.Disconnect();
                            MessageBox.Show("Дані успішно видалено");
                        }
                    }
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                db.Connect();

                var res = resDGV.Rows[cur_row].Cells[0].Value as Resource;
                string[] cols = { "resource_id", "name", "description", "units", "price" };
                string[] values = { res.Id.ToString(), DBUtil.AddQuotes(nameTB.Text), DBUtil.AddQuotes(descriptionTB.Text),
                DBUtil.AddQuotes(measureTB.Text), priceTB.Text.ToString() };

                db.UpdateRecord("resource", cols, values);
                db.Disconnect();

                MessageBox.Show("Дані успішно додано");
                getMaterials();
            } catch {
                MessageBox.Show("Деякі дані введено невірно");
            }
                       
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTB.Text) || string.IsNullOrEmpty(descriptionTB.Text) ||
                string.IsNullOrEmpty(priceTB.Text) || string.IsNullOrEmpty(measureTB.Text))
            {
                MessageBox.Show("Всі поля повинні бути заповнені", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            db.Connect();
            string[] fields = { "name", "description", "price", "units" };
            String val1 = DBUtil.AddQuotes(nameTB.Text);
            String val2 = DBUtil.AddQuotes(descriptionTB.Text);
            String val3 = Double.Parse(priceTB.Text).ToString();
            String val4 = DBUtil.AddQuotes(measureTB.Text);
            string[] values = { val1, val2, val3, val4 };
            db.InsertToBD("resource", fields, values);
            db.Disconnect();

            getMaterials();

            MessageBox.Show("Запис був успішно доданий", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void resDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cur_row= resDGV.CurrentCell.RowIndex;
            var cells = resDGV.Rows[cur_row].Cells;
            var textBoxes = panel1.Controls.OfType<TextBox>().ToArray();

            for (int i = 0; i < cells.Count; i++)
            {
                textBoxes[i].Text = cells[i].Value.ToString();
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
						Control = panel1,
						Text = "Щоб додати новий ресурс заповніть поля"
					},
					new InteractiveToolTipModel
					{
						Control = addBtn,
						Text = "Натисніть на кнопку \"Додати\""
                    },
					new InteractiveToolTipModel
					{
						Control = resDGV,
						Text = "Для редагування ресурса оберіть із списку ресурс"
					},
					new InteractiveToolTipModel
					{
						Control = panel1,
						Text = "Змініть дані"
					},
					new InteractiveToolTipModel
					{
						Control = saveBtn,
						Text = "Натисніть на кнопку \"Редагувати\""
                    },
					new InteractiveToolTipModel
					{
						Control = resDGV,
						Text = "Щоб видалити ресурс оберіть ресурс \"Видалити\""
					},
					new InteractiveToolTipModel
					{
						Control = delBtn,
						Text = "Натисніть на кнопку \"Видалити шаблон\""
                    }
                });
			}, delegate
			{
				Help.ShowHelp(this, Config.PathToHelp, HelpNavigator.Topic, "p6.html");
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