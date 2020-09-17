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

namespace Maps
{
    public partial class NormAllForm : Form
    {
        DBManager db;
        List<List<Object>> listResult;
        public NormAllForm(DBManager db)
        {
            InitializeComponent();
            this.db = db;
            listResult = db.GetRows("norm_result", "", "");
            for (int i = 0; i < listResult.Count; i++)
                dataGridView1.Rows.Add(listResult[i][0], listResult[i][1], listResult[i][2], listResult[i][3], listResult[i][4], 
                    listResult[i][5], listResult[i][6], listResult[i][7], listResult[i][8]);
        }
    }
}
