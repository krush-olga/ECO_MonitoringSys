using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Experts_Economist
{
    public partial class NormOfCalc : Form
    {
        Data.DBManager db;
        public NormOfCalc(Data.DBManager db)
        {
            this.db = db;
            InitializeComponent();
            load();
        }

        private void load()
        {
            calculationsCombo.Items.Clear();
            List<Object> names = new List<Object>();
            List<List<Object>> tmp = new List<List<Object>>();
            tmp = db.GetRows("calculations_description", "calculation_name", "id_of_expert = '2'");
            for (int i = 0; i < tmp.Count; i++)
            {
                names.Add(tmp[i][0]);
                calculationsCombo.Items.Add(names[i]);
            }
        }

        private void calculationsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<List<Object>> tmp = new List<List<Object>>();
            tmp = db.GetRows("calculations_norm", "element_name, value, result, date_of_calculation", $"calculation_number = '{db.GetValue("calculations_description", "calculation_number", $"calculation_name = '{calculationsCombo.Text}' AND id_of_expert = '2'")}'");
            for (int i = 0; i < tmp.Count; i++)
            {
                dataGridView1.Rows.Add(tmp[i][0], tmp[i][1], tmp[i][2], tmp[i][3]);
            }
            label1.Text = db.GetValue("issues", "name", $"issue_id = '{db.GetValue("calculations_description", "issue_id", $"calculation_name = '{calculationsCombo.Text}'")}'").ToString();
        }
    }
}
