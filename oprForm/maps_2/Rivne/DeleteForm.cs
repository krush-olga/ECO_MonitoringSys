using System;
using System.Collections.Generic;
using Data;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maps
{
    public partial class DeleteForm : Form
    {
        DBManager db = new DBManager();
        private List<List<Object>> list;
        bool delete = false;
        string desc, emiss, typeMarker;
        public DeleteForm(string description, string emissions, string type)
        {
            InitializeComponent();
            desc = description;
            emiss = emissions;
            typeMarker = type;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            delete = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            delete = false;
            Close();
        }

        private void DeleteForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = desc;
            textBox2.Text = emiss;
            textBox3.Text = typeMarker;
        }
        public bool CloseSelect()
        {
            return delete;
        }


    }
}
