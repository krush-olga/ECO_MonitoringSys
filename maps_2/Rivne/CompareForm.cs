using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maps
{
    public partial class CompareForm : Form
    {
        private IEnumerable<IEnumerable<object>> values;
        private IEnumerable<string> columnNames;

        public CompareForm(IEnumerable<IEnumerable<object>> values, IEnumerable<string> columnNames)
        {
            InitializeComponent();

            this.values = values;
            this.columnNames = columnNames;

            FillGrid();
        }
        

        private void FillGrid()
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            if (columnNames == null)
            {
                throw new ArgumentNullException("columnNames");
            }

            foreach (var name in columnNames)
            {
                DataGridViewTextBoxColumn dataGridViewColumn = new DataGridViewTextBoxColumn();
                dataGridViewColumn.HeaderText = name.ToString();

                CompareDataGridView.Columns.Add(dataGridViewColumn);
            }

            int count = values.Count();

            for (int i = 0; i < count; i++)
            {
                CompareDataGridView.Rows.Add(new DataGridViewRow());

                for (int j = 0; j < CompareDataGridView.Columns.Count; j++)
                {
                    CompareDataGridView.Rows[i].Cells[j].Value = values.ElementAt(i).ElementAt(j);
                }
            }
        }
    }
}
