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
		/// <summary>
		/// Attaches to the specified window. Logwindow will attach itself to the right side of the parent window and follow it around
		/// </summary>
		/// <param name="win">Designate parent window</param>
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

		/// <summary>
		/// Open the logwindow at a specified position
		/// </summary>
		/// <param name="left">Left coordinate</param>
		/// <param name="top">Top coordinate</param>
		public static void OpenAt(int left, int top)
		{
			Open();
			LogWindow.Left = left;
			LogWindow.Top = top;
		}

		/// <summary>
		/// Show the log window
		/// </summary>
		public static void Open()
		{
			LogWindow.Show();
		}

		/// <summary>
		/// When the window is detached from the parent, reattach the window
		/// </summary>
		public static void Attach()
		{
			if (_parentWindow == null) return;
			IsAttached = true;
			UpdatePosition();
		}

		/// <summary>
		/// Detach the window from the parent
		/// </summary>
		public static void Detach()
		{
			IsAttached = false;
		}

		// Settings
		/// <summary>
		/// Controls the visibility of the logwindow.
		/// </summary>
		public static Visibility Visbility
		{
			get { return LogWindow.Visibility; }
			set { LogWindow.Visibility = value; }
		}

		/// <summary>
		/// Should the logwindow have it's own taskbar icon?
		/// </summary>
		public static bool ShowInTaskbar
		{
			get { return LogWindow.ShowInTaskbar; }
			set { LogWindow.ShowInTaskbar = value; }
		}

		/// <summary>
		/// Set the left coordinate
		/// </summary>
		public static double Left
		{
			get { return LogWindow.Left; }
			set { LogWindow.Left = value; }
		}

		/// <summary>
		/// Set the top coordinate
		/// </summary>
		public static double Top
		{
			get { return LogWindow.Top; }
			set { LogWindow.Top = value; }
		}

		/// <summary>
		/// Set the width
		/// </summary>
		public static double Width
		{
			get { return LogWindow.Width; }
			set { LogWindow.Width = value; }
		}

		/// <summary>
		/// Set the height
		/// </summary>
		public static double Height
		{
			get { return LogWindow.Height; }
			set { LogWindow.Height = value; }
		}

		/// <summary>
		/// Should the log automatically include a timestamp?
		/// </summary>
		public static bool ShowTime
		{
			get { return LogWindow.ShowTime; }
			set { LogWindow.ShowTime = value; }
		}

		/// <summary>
		/// Override the time format string (default: HH:mm:ss)
		/// </summary>
		public static string TimeFormat
		{
			get { return LogWindow.TimeFormat; }
			set { LogWindow.TimeFormat = value; }
		}

		/// <summary>
		/// Override the timestamp and message sepparator. Includes spacing (default: " | ")
		/// </summary>
		public static string TimeMessageSepparator
		{
			get { return LogWindow.TimeMessageSepparator; }
			set { LogWindow.TimeMessageSepparator = value; }
		}

		public static string LogWindowHeader
		{
			get { return LogWindow.Title; }
			set { LogWindow.Title = value; }
		}

		// Write methods
		/// <summary>
		/// Writes to specified text to the log using a specific level
		/// </summary>
		/// <param name="level">Severity level</param>
		/// <param name="text">Text that should be written</param>
		public static void Log(LogLevel level, string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(level));
		}

		/// <summary>
		/// Write a message with a custom style to the log
		/// </summary>
		/// <param name="text">Text that should be written</param>
		/// <param name="style">A object containing the styling information</param>
		public static void Log(string text, LogStyle style)
		{
			LogWindow.StyledMessage(text, style);
		}

		// Quick log methods
		/// <summary>
		/// Writes a Debug message to the log
		/// </summary>
		/// <param name="text"></param>
		public static void Debug(string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(LogLevel.Debug));
		}

		/// <summary>
		/// Writes a Message message to the log
		/// </summary>
		/// <param name="text"></param>
		public static void Message(string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(LogLevel.Message));
		}

		/// <summary>
		/// Writes a Info message to the log
		/// </summary>
		/// <param name="text"></param>
		public static void Info(string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(LogLevel.Info));
		}

		/// <summary>
		/// Writes a Warning message to the log
		/// </summary>
		/// <param name="text"></param>
		public static void Warning(string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(LogLevel.Warning));
		}

		/// <summary>
		/// Writes a Error message to the log
		/// </summary>
		/// <param name="text"></param>
		public static void Error(string text)
		{
			LogWindow.StyledMessage(text, LogStyle.GetLogStyle(LogLevel.Error));
		}

		// Private methods
		/// <summary>
		/// Updates the position in correspondance to the parent.
		/// </summary>
		private static void UpdatePosition()
		{
			if (IsAttached == false) return;
			LogWindow.Left = _parentWindow.Left + _parentWindow.Width;
			LogWindow.Top = _parentWindow.Top;
		}
	}
}
