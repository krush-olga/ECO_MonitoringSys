using System;
using System.Drawing;
using System.Windows.Forms;

namespace HelpModule.Controls
{
	public class GrowLabel : Label
	{
		private bool _mGrowing;
		public GrowLabel()
		{
			AutoSize = false;
		}
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
	}
}
