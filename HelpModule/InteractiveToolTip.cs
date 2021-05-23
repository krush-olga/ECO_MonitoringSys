#region License

//
// InteractiveToolTip.cs
//
// Copyright (C) 2012-2013 Alex Taylor.  All Rights Reserved.
//
// InteractiveToolTip is published under the terms of the Code Project Open License.
// http://www.codeproject.com/info/cpol10.aspx
//

#endregion License

namespace HelpModule.Controls
{
	#region Usings

	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Windows.Forms;

	#endregion Usings

	/// <summary>
	/// Represents a balloon-style <see cref="T:System.Windows.Forms.ToolTip"/> which supports caller-supplied interactive content.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>InteractiveToolTip</b> offers similar behaviour to the modal functionality of <see cref="T:System.Windows.Forms.ToolTip"/>, but replaces the text-content
	/// with a <see cref="T:System.Windows.Forms.Control"/>, which may be used to provide either more complex formatting than is possible with <see cref="T:System.Windows.Forms.ToolTip"/>,
	/// or to implement an interactive control model.
	/// </para>
	/// <para>
	/// The <see cref="T:System.Windows.Forms.Control"/> may be anything you like. Transparent <see cref="F:System.Windows.Forms.Control.BackColor"/>s are supported.
	/// </para>
	/// </remarks>
	public partial class InteractiveToolTip : Component
	{
		#region Inner types

		public enum StemPosition
		{
			BottomLeft,
			BottomCentre,
			BottomRight,
			TopLeft,
			TopCentre,
			TopRight
		}

		private sealed class Win32
		{
			public const string TOOLTIPS_CLASS = "tooltips_class32";
			public const int TTS_ALWAYSTIP = 0x01;
			public const int TTS_NOFADE = 0x10;
			public const int TTS_NOANIMATE = 0x20;
			public const int TTS_BALLOON = 0x40;
			public const int TTF_IDISHWND = 0x0001;
			public const int TTF_CENTERTIP = 0x0002;
			public const int TTF_TRACK = 0x0020;
			public const int TTF_TRANSPARENT = 0x0100;
			public const int WM_SETFONT = 0x30;
			public const int WM_GETFONT = 0x31;
			public const int WM_PRINTCLIENT = 0x318;
			public const int WM_USER = 0x0400;
			public const int TTM_TRACKACTIVATE = WM_USER + 17;
			public const int TTM_TRACKPOSITION = WM_USER + 18;
			public const int TTM_SETMAXTIPWIDTH = WM_USER + 24;
			public const int TTM_GETBUBBLESIZE = WM_USER + 30;
			public const int TTM_ADDTOOL = WM_USER + 50;
			public const int TTM_DELTOOL = WM_USER + 51;
			public const int SWP_NOSIZE = 0x0001;
			public const int SWP_NOACTIVATE = 0x0010;
			public const int SWP_NOOWNERZORDER = 0x200;
			public readonly static IntPtr HWND_TOPMOST = new IntPtr(-1);

			[StructLayout(LayoutKind.Sequential)]
			public struct RECT
			{
				public int left;
				public int top;
				public int right;
				public int bottom;
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct TOOLINFO
			{
				public int cbSize;
				public int uFlags;
				public IntPtr hwnd;
				public IntPtr uId;
				public RECT rect;
				public IntPtr hinst;

				[MarshalAs(UnmanagedType.LPTStr)] public string lpszText;

				public System.UInt32 lParam;
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct SIZE
			{
				public int cx;
				public int cy;
			}

			[DllImport("User32", SetLastError = true)]
			public static extern int GetWindowRect(IntPtr hWnd, ref RECT lpRect);

			[DllImport("User32", SetLastError = true)]
			public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref TOOLINFO lParam);

			[DllImport("User32", SetLastError = true)]
			public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, out RECT lParam);

			[DllImport("User32", SetLastError = true)]
			public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

			[DllImport("User32", SetLastError = true)]
			public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy,
				int uFlags);

			[DllImport("User32", SetLastError = true)]
			public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		}

		private class ContentPanel : UserControl
		{
			private IntPtr _toolTipHwnd;

			public ContentPanel(IntPtr toolTipHWnd)
			{
				_toolTipHwnd = toolTipHWnd;
				Win32.SetParent(Handle, toolTipHWnd);
			}

			protected override void OnPaintBackground(PaintEventArgs e)
			{
				Win32.SendMessage(_toolTipHwnd, Win32.WM_PRINTCLIENT, (int) e.Graphics.GetHdc(), 0);
			}
		}

		private class ToolTipWindow : NativeWindow, IDisposable
		{
			#region Internals

			private const int StemInset = 16;

			private static StringFormat StringFormat = new StringFormat(StringFormat.GenericTypographic)
				{FormatFlags = StringFormatFlags.MeasureTrailingSpaces};

			private ContentPanel _contentPanel;
			private Win32.TOOLINFO _toolInfo;
			private bool _mouseOverToolTip;

			private Win32.TOOLINFO CreateTool(string contentSpacing, IWin32Window window, StemPosition stemPosition)
			{
				Win32.TOOLINFO ti = new Win32.TOOLINFO();

				ti.cbSize = Marshal.SizeOf(ti);
				ti.uFlags = Win32.TTF_IDISHWND | Win32.TTF_TRACK | Win32.TTF_TRANSPARENT;
				ti.uId = window.Handle;
				ti.hwnd = window.Handle;
				ti.lpszText = contentSpacing;

				if (StemPosition.BottomCentre == stemPosition || StemPosition.TopCentre == stemPosition)
					ti.uFlags |= Win32.TTF_CENTERTIP;

				if (0 == Win32.SendMessage(Handle, Win32.TTM_ADDTOOL, 0, ref ti))
					throw new Exception();

				Win32.SendMessage(Handle, Win32.TTM_SETMAXTIPWIDTH, 0, SystemInformation.MaxWindowTrackSize.Width);

				return ti;
			}

			private StemPosition AdjustStemPosition(StemPosition stemPosition, ref Rectangle toolTipBounds,
				ref Rectangle screenBounds)
			{
				if (toolTipBounds.Left < screenBounds.Left)
				{
					if (StemPosition.TopCentre == stemPosition || StemPosition.TopRight == stemPosition)
						stemPosition = StemPosition.TopLeft;
					else if (StemPosition.BottomCentre == stemPosition || StemPosition.BottomRight == stemPosition)
						stemPosition = StemPosition.BottomLeft;
				}
				else if (toolTipBounds.Right > screenBounds.Right)
				{
					if (StemPosition.TopCentre == stemPosition || StemPosition.TopLeft == stemPosition)
						stemPosition = StemPosition.TopRight;
					else if (StemPosition.BottomCentre == stemPosition || StemPosition.BottomLeft == stemPosition)
						stemPosition = StemPosition.BottomRight;
				}

				if (toolTipBounds.Top < screenBounds.Top)
				{
					switch (stemPosition)
					{
						case StemPosition.BottomLeft:
							stemPosition = StemPosition.TopLeft;
							break;

						case StemPosition.BottomCentre:
							stemPosition = StemPosition.TopCentre;
							break;

						case StemPosition.BottomRight:
							stemPosition = StemPosition.TopRight;
							break;
					}
				}
				else if (toolTipBounds.Bottom > screenBounds.Bottom)
				{
					switch (stemPosition)
					{
						case StemPosition.TopLeft:
							stemPosition = StemPosition.BottomLeft;
							break;

						case StemPosition.TopCentre:
							stemPosition = StemPosition.BottomCentre;
							break;

						case StemPosition.TopRight:
							stemPosition = StemPosition.BottomRight;
							break;
					}
				}

				return stemPosition;
			}

			private Rectangle CalculateToolTipLocation(string contentSpacing, IWin32Window window, int x, int y,
				StemPosition stemPosition)
			{
				Rectangle toolTipBounds = new Rectangle();
				Size toolTipSize = GetToolTipWindowSize(contentSpacing);
				Win32.RECT windowBounds = new Win32.RECT();

				Win32.GetWindowRect(window.Handle, ref windowBounds);
				x += windowBounds.left;

				if (StemPosition.TopLeft == stemPosition || StemPosition.BottomLeft == stemPosition)
					toolTipBounds.X = x - StemInset;
				else if (StemPosition.TopCentre == stemPosition || StemPosition.BottomCentre == stemPosition)
					toolTipBounds.X = x - (toolTipSize.Width / 2);
				else
					toolTipBounds.X = x - toolTipSize.Width + StemInset;

				if (StemPosition.TopLeft == stemPosition || StemPosition.TopCentre == stemPosition ||
					StemPosition.TopRight == stemPosition)
					toolTipBounds.Y = windowBounds.bottom - y;
				else
					toolTipBounds.Y = y + windowBounds.top - toolTipSize.Height;

				toolTipBounds.Width = toolTipSize.Width;
				toolTipBounds.Height = toolTipSize.Height;

				return toolTipBounds;
			}

			private Size GetToolTipWindowSize(string contentSpacing)
			{
				Win32.TOOLINFO ti = new Win32.TOOLINFO();
				ti.cbSize = Marshal.SizeOf(ti);
				ti.uFlags = Win32.TTF_TRACK;
				ti.lpszText = contentSpacing;

				if (0 == Win32.SendMessage(Handle, Win32.TTM_ADDTOOL, 0, ref ti))
					throw new Exception();

				Win32.SendMessage(Handle, Win32.TTM_SETMAXTIPWIDTH, 0, SystemInformation.MaxWindowTrackSize.Width);
				Win32.SendMessage(Handle, Win32.TTM_TRACKACTIVATE, 1, ref ti);

				Win32.RECT rect = new Win32.RECT();
				Win32.GetWindowRect(Handle, ref rect);

				Win32.SendMessage(Handle, Win32.TTM_TRACKACTIVATE, 0, ref ti);
				Win32.SendMessage(Handle, Win32.TTM_DELTOOL, 0, ref ti);

				return new Size(rect.right - rect.left, rect.bottom - rect.top);
			}

			private void DoLayout(IWin32Window window, Control content, StemPosition stemPosition,
				ref Rectangle toolTipBounds)
			{
				int bubbleSize = Win32.SendMessage(Handle, Win32.TTM_GETBUBBLESIZE, 0, ref _toolInfo);
				int bubbleWidth = bubbleSize & 0xFFFF;
				int bubbleHeight = bubbleSize >> 16;

				content.Left = (bubbleWidth - content.Width) / 2;

				if (StemPosition.BottomLeft == stemPosition || StemPosition.BottomCentre == stemPosition ||
					StemPosition.BottomRight == stemPosition)
				{
					content.Top = (bubbleHeight - content.Height) / 2;
				}
				else
				{
					int bubbleOffset = toolTipBounds.Height - bubbleHeight;
					content.Top = (bubbleHeight - content.Height) / 2 + bubbleOffset;
				}

				_contentPanel = new ContentPanel(Handle);
				_contentPanel.Width = toolTipBounds.Width;
				_contentPanel.Height = toolTipBounds.Height;
				_contentPanel.Controls.Add(content);

				Win32.SetWindowPos(Handle, Win32.HWND_TOPMOST, toolTipBounds.X, toolTipBounds.Y, 0, 0,
					Win32.SWP_NOACTIVATE | Win32.SWP_NOSIZE | Win32.SWP_NOOWNERZORDER);
			}

			private string GetSizingText(Control content)
			{
				StringBuilder sb = new StringBuilder();
				Graphics graphics = Graphics.FromHwnd(Handle);
				Font font = Font.FromHfont((IntPtr) Win32.SendMessage(Handle, Win32.WM_GETFONT, 0, 0));

				font = new Font(font.FontFamily, 1.0f);
				Win32.SendMessage(Handle, Win32.WM_SETFONT, (int) font.ToHfont(), 1);

				Size size = TextRenderer.MeasureText(" ", font);
				int rows = (content.Height + size.Height - 1) / size.Height;

				for (int n = 0; n < rows; n++)
				{
					sb.Append("\r\n");
				}

				size = TextRenderer.MeasureText(sb.ToString(), font);

				int width = content.Width + size.Height - content.Height;

				while (size.Width < width)
				{
					sb.Append(" ");
					size = TextRenderer.MeasureText(sb.ToString(), font);
				}

				return sb.ToString();
			}

			#endregion Internals

			#region Constructor

			public ToolTipWindow(Control content, IWin32Window window, int x, int y, StemPosition stemPosition,
				bool useAnimation, bool useFading)
			{
				Window = window;

				CreateParams createParams = new CreateParams();
				createParams.ClassName = Win32.TOOLTIPS_CLASS;
				createParams.Style = Win32.TTS_ALWAYSTIP | Win32.TTS_BALLOON;

				if (!useAnimation)
					createParams.Style |= Win32.TTS_NOANIMATE;

				if (!useFading)
					createParams.Style |= Win32.TTS_NOFADE;

				CreateHandle(createParams);

				string contentSpacing = GetSizingText(content);

				Rectangle toolTipBounds = CalculateToolTipLocation(contentSpacing, Window, x, y, stemPosition);
				Screen currentScreen = Screen.FromHandle(Window.Handle);
				Rectangle screenBounds = currentScreen.WorkingArea;

				stemPosition = AdjustStemPosition(stemPosition, ref toolTipBounds, ref screenBounds);

				toolTipBounds = CalculateToolTipLocation(contentSpacing, Window, x, y, stemPosition);
				toolTipBounds.X = Math.Max(0, toolTipBounds.X);
				toolTipBounds.Y = Math.Max(0, toolTipBounds.Y);

				_toolInfo = CreateTool(contentSpacing, Window, stemPosition);

				int initialX = screenBounds.X;
				int initialY = screenBounds.Y;

				if (StemPosition.TopLeft == stemPosition || StemPosition.BottomLeft == stemPosition)
					initialX += StemInset;
				else if (StemPosition.TopCentre == stemPosition || StemPosition.BottomCentre == stemPosition)
					initialX += screenBounds.Width / 2;
				else
					initialX += screenBounds.Width - StemInset;

				if (StemPosition.BottomLeft == stemPosition || StemPosition.BottomCentre == stemPosition ||
					StemPosition.BottomRight == stemPosition)
					initialY += screenBounds.Height;

				Win32.SendMessage(Handle, Win32.TTM_TRACKPOSITION, 0, (initialY << 16) | initialX);

				// and finally display it
				Win32.SendMessage(Handle, Win32.TTM_TRACKACTIVATE, 1, ref _toolInfo);
				DoLayout(Window, content, stemPosition, ref toolTipBounds);

				_contentPanel.MouseEnter += delegate(object sender, EventArgs e)
				{
					if (null != MouseEnter && !_mouseOverToolTip)
					{
						_mouseOverToolTip = true;
						MouseEnter(this, e);
					}
				};

				_contentPanel.MouseLeave += delegate(object sender, EventArgs e)
				{
					if (null != MouseLeave && _mouseOverToolTip && null ==
						_contentPanel.GetChildAtPoint(_contentPanel.PointToClient(Control.MousePosition)))
					{
						_mouseOverToolTip = false;
						MouseLeave(this, e);
					}
				};
			}

			~ToolTipWindow()
			{
				Dispose(false);
			}

			public void Dispose()
			{
				Dispose(true);
			}

			private void Dispose(bool disposing)
			{
				if (disposing)
				{
					Win32.SendMessage(Handle, Win32.TTM_TRACKACTIVATE, 0, ref _toolInfo);
					Win32.SendMessage(Handle, Win32.TTM_DELTOOL, 0, ref _toolInfo);

					if (null != _contentPanel)
					{
						_contentPanel.Controls.Clear();
						_contentPanel.Dispose();
					}

					DestroyHandle();
				}
			}

			#endregion Constructor

			#region API

			public IWin32Window Window;

			public event EventHandler MouseEnter;

			public event EventHandler MouseLeave;

			#endregion API
		}

		#endregion Inner types

		#region Internals

		private Timer _durationTimer;
		private ToolTipWindow _currentToolTip;

		#endregion Internals

		#region Properties

		public bool UseAnimation { get; set; }

		public bool UseFading { get; set; }

		#endregion Properties

		#region Constructor

		public InteractiveToolTip()
		{
			InitializeComponent();
			UseAnimation = true;
			UseFading = true;

			_durationTimer = new Timer();
			components.Add(_durationTimer);

			_durationTimer.Tick += delegate(object sender, EventArgs e) { Hide(); };
		}

		public InteractiveToolTip(IContainer container)
			: this()
		{
			container.Add(this);
		}

		~InteractiveToolTip()
		{
			Dispose(false);
		}

		#endregion Constructor

		#region API

		public void Show(Control content, IWin32Window window)
		{
			Show(content, window, StemPosition.BottomLeft);
		}


		public void Show(Control content, IWin32Window window, StemPosition stemPosition)
		{
			Show(content, window, 0, 0, stemPosition, 0);
		}


		public void Show(Control content, IWin32Window window, Point location)
		{
			Show(content, window, location.X, location.Y);
		}


		public void Show(Control content, IWin32Window window, int x, int y)
		{
			Show(content, window, x, y, StemPosition.BottomLeft, 0);
		}


		public void Show(Control content, IWin32Window window, Point location, StemPosition stemPosition, int duration)
		{
			Show(content, window, location.X, location.Y, stemPosition, duration);
		}


		public void Show(Control content, IWin32Window window, int x, int y, StemPosition stemPosition, int duration)
		{
			if (null == content || null == window)
				throw new ArgumentNullException();

			Hide();
			_currentToolTip = new ToolTipWindow(content, window, x, y, stemPosition, UseAnimation, UseFading);

			if (duration > 0)
			{
				_currentToolTip.MouseEnter += delegate(object sender, EventArgs e) { _durationTimer.Stop(); };

				_currentToolTip.MouseLeave += delegate(object sender, EventArgs e)
				{
					if (duration > 0)
						_durationTimer.Start();
				};

				_durationTimer.Interval = duration;
				_durationTimer.Start();
			}

			ToolTipShown?.Invoke(this, new InteractiveToolTipEventArgs(window));
		}


		public void Hide()
		{
			_durationTimer.Stop();

			ToolTipWindow toolTip = _currentToolTip;
			IWin32Window window;

			if (null != toolTip)
			{
				_currentToolTip = null;
				window = toolTip.Window;
				toolTip.Dispose();

				ToolTipHidden?.Invoke(this, new InteractiveToolTipEventArgs(window));
			}
		}


		public event InteractiveToolTipEventHandler ToolTipShown;

		public event InteractiveToolTipEventHandler ToolTipHidden;

		#endregion API
	}


	public delegate void InteractiveToolTipEventHandler(object sender, InteractiveToolTipEventArgs e);

	public class InteractiveToolTipEventArgs : EventArgs
	{
		public IWin32Window Window { get; private set; }

		public InteractiveToolTipEventArgs(IWin32Window window)
			: base()
		{
			Window = window;
		}
	}
}