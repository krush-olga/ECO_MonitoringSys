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

namespace Experts_Economist
{
    public partial class MarkersForm : Form
    {
        ConnectDB connection;
        DBManager db;
        List<List<Object>> markersList = null;
        int id;
        string fileName;
        Stream fileStream;
        public MarkersForm(ConnectDB connection, DBManager db)
        {
            InitializeComponent();
            this.connection = connection;
            this.db = db;
        }

        private void MarkersForm_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                MessageBox.Show("Всі поля воині бути заповнені");
            else
            {
                db.InsertImageToBD("type_of_object","'" + id.ToString() + "', '" + textBox1.Text + "',@file, '" + fileName + "'", textBox2.Text);
                textBox1.Text = "";
                textBox2.Text = "";
            }
            LoadGrid();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Image files (*.png, *.btm)|*.png;*.btm";
            openFileDialog1.FileName = "";
            openFileDialog1.RestoreDirectory = true;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
                fileName = openFileDialog1.SafeFileName;
                fileStream = openFileDialog1.OpenFile();
            }
        }
        private void LoadGrid()
        {
            markersList = db.GetRows("type_of_object", "", "");

            if (markersList.Count == 0)
            {
                return;
            }

            id = (int)markersList[markersList.Count - 1][0] + 1;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < markersList.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = markersList[i][0];
                dataGridView1.Rows[i].Cells[1].Value = markersList[i][1];
                dataGridView1.Rows[i].Cells[2].Value = markersList[i][2];
                dataGridView1.Rows[i].Cells[3].Value = markersList[i][3];
            }
        }
    }
}
