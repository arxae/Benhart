using System.Windows;
using BenhartLog.Windows;

namespace BenhartLog
{
	public static class Benhart
	{
		private static Window _parentWindow;
		private static bool IsAttached;

		private static BenhartLogWindow _instance;
		private static BenhartLogWindow LogWindow { get { return _instance ?? (_instance = new BenhartLogWindow()); } }

		// Log control methods
		public static void AttachTo(Window win)
		{
			_parentWindow = win;
			_parentWindow.LocationChanged += (s, e) => UpdatePosition();
			_parentWindow.SizeChanged += (s, e) => UpdatePosition();
			_parentWindow.Closing += (s, e) => // do not close the window when it is not attached
			{
				if (IsAttached) LogWindow.Close();
				else _parentWindow = null;
			};

			IsAttached = true;
			UpdatePosition();
			LogWindow.Show();
		}

		public static void Open()
		{
			LogWindow.Show();
		}

		public static void OpenAt(int left, int top)
		{
			Open();
			LogWindow.Left = left;
			LogWindow.Top = top;
		}

		public static void Attach()
		{
			if (_parentWindow == null) return;
			IsAttached = true;
			UpdatePosition();
		}

		public static void Detach()
		{
			IsAttached = false;
		}

		// Settings
		public static Visibility Visbility
		{
			get { return LogWindow.Visibility; }
			set { LogWindow.Visibility = value; }
		}

		public static bool ShowInTaskbar
		{
			get { return LogWindow.ShowInTaskbar; }
			set { LogWindow.ShowInTaskbar = value; }
		}

		public static double Left
		{
			get { return LogWindow.Left; }
			set { LogWindow.Left = value; }
		}

		public static double Top
		{
			get { return LogWindow.Top; }
			set { LogWindow.Top = value; }
		}

		public static double Width
		{
			get { return LogWindow.Width; }
			set { LogWindow.Width = value; }
		}

		public static double Height
		{
			get { return LogWindow.Height; }
			set { LogWindow.Height = value; }
		}

		public static bool ShowTime
		{
			get { return LogWindow.ShowTime; }
			set { LogWindow.ShowTime = value; }
		}

		public static string TimeFormat
		{
			get { return LogWindow.TimeFormat; }
			set { LogWindow.TimeFormat = value; }
		}

		public static string TimeMessageSepparator
		{
			get { return LogWindow.TimeMessageSepparator; }
			set { LogWindow.TimeMessageSepparator = value; }
		}

		// Write methods
		public static void Log(LogLevel level, string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(level));
		}

		public static void Log(string text, LogStyle style)
		{
			LogWindow.StyledMessage(text, style);
		}

		public static void Debug(string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(LogLevel.Debug));
		}

		public static void Message(string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(LogLevel.Message));
		}

		public static void Info(string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(LogLevel.Info));
		}

		public static void Warning(string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(LogLevel.Warning));
		}

		public static void Error(string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(LogLevel.Error));
		}

		private static void UpdatePosition()
		{
			if (IsAttached == false) return;
			LogWindow.Left = _parentWindow.Left + _parentWindow.Width;
			LogWindow.Top = _parentWindow.Top;
		}
	}
}
