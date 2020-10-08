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
using System.IO;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math.Field;

namespace Experts_Economist
{
    public partial class MarkersForm : Form
    {
        private int id;
        private DBManager db;
        private ConnectDB connection;
        private List<List<Object>> markersList;

        public MarkersForm(ConnectDB connection, DBManager db)
        {
            InitializeComponent();

            this.connection = connection;
            markersList = null;
            this.db = db;
        }

        private void MarkersForm_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text) || string.IsNullOrEmpty(kvedTextBox.Text) || string.IsNullOrEmpty(imageNameTextBox.Text))
            {
                MessageBox.Show("Всі поля воині бути заповнені.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string[] field = { "id", "kved", "name", "image_name" };
                string[] values = { id.ToString(), kvedTextBox.Text, DBUtil.AddQuotes(nameTextBox.Text), DBUtil.AddQuotes(imageNameTextBox.Text) };

                db.InsertToBD("type_of_object", field, values);

                MessageBox.Show("Запис успішно додан.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadGrid();
        }

        private void LoadGrid()
        {
            markersList = db.GetRows("type_of_object", "id, kved, name, image_name", "");

            if (markersList.Count == 0)
            {
                return;
            }

            id = (int)markersList[markersList.Count - 1][0] + 1;

            dataGridView1.Rows.Clear();

            for (int i = 0; i < markersList.Count; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < markersList[i].Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = markersList[i][j];
                }
            }
        }

        private void deleteFromDBButton_Click(object sender, EventArgs e)
        {
            DialogResult promptRes = MessageBox.Show($"Ви впевнені, що хочете видалити запис з id {dataGridView1.CurrentRow.Cells[0].Value}?", "Увага", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            if (promptRes == DialogResult.No)
            {
                MessageBox.Show("Операція відмінена.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            db.DeleteFromDB("type_of_object", "id", dataGridView1.CurrentRow.Cells[0].Value.ToString());

            MessageBox.Show("Запис успішно видалений.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);

            nameTextBox.Text = "";
            kvedTextBox.Text = "";
            imageNameTextBox.Text = "";

            LoadGrid();
        }

        private void editBbutton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Виберіть запис.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(nameTextBox.Text) || string.IsNullOrEmpty(kvedTextBox.Text) ||
                string.IsNullOrEmpty(imageNameTextBox.Text))
            {
                MessageBox.Show("Всі поля воині бути заповнені.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            DialogResult promptRes = MessageBox.Show($"Ви впевнені, що хочете змінити запис з квед {dataGridView1.CurrentRow.Cells[0].Value}?", "Увага", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (promptRes == DialogResult.No)
            {
                MessageBox.Show("Операція відмінена.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string[] updateFields = { "id", "kved", "name", "image_name" };
            string[] updateValues = { dataGridView1.CurrentRow.Cells[0].Value.ToString(), kvedTextBox.Text, DBUtil.AddQuotes(nameTextBox.Text), DBUtil.AddQuotes(imageNameTextBox.Text) };

            db.UpdateRecord("type_of_object", updateFields, updateValues);

            MessageBox.Show("Запис успішно змінений.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadGrid();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            kvedTextBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            nameTextBox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            imageNameTextBox.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
