using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Maps
{

    public partial class Elements : Form
    {
    private Data.DBManager db = new Data.DBManager();
        private string _name = "zzzzzzzzz";
        private bool dataClose = false;
        public Elements()
        {
            InitializeComponent();
        }

        private void Elements_Load(object sender, EventArgs e)
        {
            List<List<Object>> listElements;
            listElements = db.GetRows("elements", "code, name, measure, rigid, voc, hydrocarbon, formula, cas", "");
            for (int i =0; i<listElements.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value=(listElements[i][0]);
                row.Cells[1].Value=(listElements[i][1]);
                row.Cells[2].Value=(listElements[i][2]);
                row.Cells[3].Value=(listElements[i][3]);
                row.Cells[4].Value=(listElements[i][4]);
                row.Cells[5].Value=(listElements[i][5]);
                row.Cells[6].Value=(listElements[i][6]);
                row.Cells[7].Value = (listElements[i][7]);
                dataGridView1.Rows.Add(row);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataClose = true;
            CloseOK();
            this.Close();
        }
        public string CloseOK()
        {
            if (dataClose)
            {
                _name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                return _name;
            }
            else
                return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataClose = false;

            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
