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
    public partial class AddNewSeriesWindow : Form
    {
        public string SeriesName { get; private set; }
        public string SeriesDescription { get; private set; }

        public AddNewSeriesWindow()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            SeriesName = SeriesTextBox.Text;
            SeriesDescription = DescriptionTextBox.Text;
        }
    }
}
