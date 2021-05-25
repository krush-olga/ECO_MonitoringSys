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
        public AddNewSeriesWindow()
        {
            InitializeComponent();
        }

		private void SeriesTextBox_Click(object sender, EventArgs e)
		{
			new ToolTip().Show("Введіть назву серії (до 250 символів)", (Control)sender, 0, ((Control)sender).Height, 2000);
		}

		private void DescriptionTextBox_Click(object sender, EventArgs e)
		{
			new ToolTip().Show("Введіть опис серії (до 500 символів)", (Control)sender, 0, ((Control)sender).Height, 2000);
		}
	}
}
