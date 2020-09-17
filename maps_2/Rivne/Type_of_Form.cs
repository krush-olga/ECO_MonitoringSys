using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odessa
{
    public partial class Type_of_Form : Form
    {
        int type = 1;
        public Type_of_Form()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            type = 1;
            this.Close();

        }

        public void Type_of_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            ChoiceMode();

        }

        public int ChoiceMode()
        {
            return type;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            type = 2;
            this.Close();
        }
    }
}
