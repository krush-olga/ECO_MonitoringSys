using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HelpModule
{
	public class InteractiveToolTipModel
	{
		public string Text { get; set; }

		public Control Control { get; set; }

		public bool IsNotFinal { get; set; }

		public Action AfterHandler { get; set; }
	}
}
