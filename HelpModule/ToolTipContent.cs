using System;
using System.Drawing;
using System.Windows.Forms;

namespace HelpModule.Controls
{
	public partial class ToolTipContent : UserControl
	{
		#region Constructors

		public ToolTipContent()
		{
			InitializeComponent();
			CustomInitControls();
		}

		#endregion

		#region Methods : Private

		private void CustomInitControls()
		{
			LinkLabel.Location = new Point(0, Height - LinkLabel.Height);
			Message.Location = new Point(0, 0);
		}

		private void Message_TextChanged(object sender, EventArgs e)
		{
			Height = Message.PreferredHeight + 5 + LinkLabel.Height;
			LinkLabel.Location = new Point(0, Height - LinkLabel.Height);
		}

		#endregion

	}
}
