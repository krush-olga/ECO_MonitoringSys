using System;
using System.Collections.Generic;
using System.Drawing;
using HelpModule.Controls;

namespace HelpModule
{
	public class InteractiveToolTipCreator
	{
		#region Fields : Private

		private List<InteractiveToolTip> _interactiveToolTips;
		private List<ToolTipContent> _toolTipContents;
		private const int _offset = 8;

		#endregion

		#region Methods : Private

		public void CreateTips(List<InteractiveToolTipModel> items)
		{
			_interactiveToolTips = new List<InteractiveToolTip>();
			_toolTipContents = new List<ToolTipContent>();
			foreach (InteractiveToolTipModel item in items)
			{
				_interactiveToolTips.Add(new InteractiveToolTip());
				_toolTipContents.Add(new ToolTipContent
				{
					BackColor = Color.Transparent,
					Message = {Text = item.Text}
				});
			}

			for (var i = 0; i < items.Count; i++)
			{
				var currentItem = items[i];
				var ttc = _toolTipContents[i];
				var itt = _interactiveToolTips[i];
				if (i + 1 != items.Count)
				{
					var ittNext = _interactiveToolTips[i + 1];
					var ttcNext = _toolTipContents[i + 1];
					var nextItem = items[i + 1];
					ttc.LinkLabel.LinkClicked += delegate
					{
						try
						{
							currentItem.AfterHandler?.Invoke();
							itt.Hide();
							ittNext.Show(ttcNext, nextItem.Control, nextItem.Control.Width - _offset, 0);
						}
						catch
						{
							// ignored
						}
					};
				}
				else
				{
					if (!currentItem.IsNotFinal)
						ttc.LinkLabel.Text = "Завершити";
					ttc.LinkLabel.LinkClicked += delegate
					{
						try
						{
							currentItem.AfterHandler?.Invoke();
							itt.Hide();
						}
						catch
						{
							// ignored
						}
					};
				}
			}

			_interactiveToolTips[0].Show(_toolTipContents[0], items[0].Control, items[0].Control.Width - _offset, 0);
		}

		#endregion
	}
}
