using System;
using System.Windows.Forms;

namespace HelpModule
{
	public partial class HelpToolTipForm : Form
	{
		private readonly Action _yes;
		private readonly Action _no;
		public HelpToolTipForm(Action yes, Action no)
		{
			InitializeComponent();
			_yes = yes;
			_no = no;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_yes.Invoke();
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			_no.Invoke();
			Close();
		}
	}
}
