using System;
using System.Drawing;
using System.Windows.Forms;

namespace HelpModule.Controls
{
	public class GrowLabel : Label
	{
		#region Fields : Private

		private bool _mGrowing;

		#endregion

		#region Constructors

		public GrowLabel()
		{
			AutoSize = false;
		}

		#endregion

		#region Methods : Protected

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			ResizeLabel();
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			ResizeLabel();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			ResizeLabel();
		}

		#endregion

		#region Methods : Private

		private void ResizeLabel()
		{
			if (_mGrowing) return;
			try
			{
				_mGrowing = true;
				var sz = new Size(Width, int.MaxValue);
				sz = TextRenderer.MeasureText(Text, Font, sz, TextFormatFlags.WordBreak);
				Height = sz.Height;
			}
			finally
			{
				_mGrowing = false;
			}
		}

		#endregion
	}
}
