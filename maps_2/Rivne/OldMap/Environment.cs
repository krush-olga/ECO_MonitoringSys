using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maps.OldMap
{
    public partial class Environment : Form
    {
        private Data.DBManager db = new Data.DBManager();
        private bool dataClose = false;
        public Environment()
        {
            InitializeComponent();
        }

        private void Environment_Load(object sender, EventArgs e)
        {
            List<List<Object>> listElements;
            listElements = db.GetRows("environment", "name, id", "");
            for (int i = 0; i < listElements.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = (listElements[i][0]);
                row.Cells[1].Value = (listElements[i][1]);
                dataGridView1.Rows.Add(row);
            }
        }
        public DataGridViewRow CloseOK()
        {
            if (dataClose)
            {
                int index = dataGridView1.CurrentRow.Index;
                return dataGridView1.Rows[index];
            }
            else
            {
                return null;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataClose = true;
            CloseOK();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataClose = false;
            this.Close();
        }
    }
}
