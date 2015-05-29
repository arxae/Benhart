using System.Windows;

namespace BenhartLog
{
	public class BenhartWindowOptions
	{
		// ===		WINDOW OPTIONS	===
		/// <summary>
		/// Controls the visibility of the logwindow.
		/// </summary>
		public static Visibility Visbility
		{
			get { return Benhart.Window.Visibility; }
			set { Benhart.Window.Visibility = value; }
		}

		/// <summary>
		/// Should the logwindow have it's own taskbar icon?
		/// </summary>
		public static bool ShowInTaskbar
		{
			get { return Benhart.Window.ShowInTaskbar; }
			set { Benhart.Window.ShowInTaskbar = value; }
		}

		/// <summary>
		/// Set the left coordinate
		/// </summary>
		public static double Left
		{
			get { return Benhart.Window.Left; }
			set { Benhart.Window.Left = value; }
		}

		/// <summary>
		/// Set the top coordinate
		/// </summary>
		public static double Top
		{
			get { return Benhart.Window.Top; }
			set { Benhart.Window.Top = value; }
		}

		/// <summary>
		/// Set the width
		/// </summary>
		public static double Width
		{
			get { return Benhart.Window.Width; }
			set { Benhart.Window.Width = value; }
		}

		/// <summary>
		/// Set the height
		/// </summary>
		public static double Height
		{
			get { return Benhart.Window.Height; }
			set { Benhart.Window.Height = value; }
		}

		/// <summary>
		/// Should the log automatically include a timestamp?
		/// </summary>
		public static bool ShowTimestamp
		{
			get { return Benhart.LogPage.ShowTime; }
			set { Benhart.LogPage.ShowTime = value; }
		}

		/// <summary>
		/// Override the time format string (default: HH:mm:ss)
		/// </summary>
		public static string TimeFormat
		{
			get { return Benhart.LogPage.TimeFormat; }
			set { Benhart.LogPage.TimeFormat = value; }
		}

		/// <summary>
		/// Override the timestamp and message sepparator. Includes spacing (default: " | ")
		/// </summary>
		public static string TimeMessageSepparator
		{
			get { return Benhart.LogPage.TimeMessageSepparator; }
			set { Benhart.LogPage.TimeMessageSepparator = value; }
		}

		/// <summary>
		/// Set the window header
		/// </summary>
		public static string LogWindowHeader
		{
			get { return Benhart.Window.Title; }
			set { Benhart.Window.Title = value; }
		}

		/// <summary>
		/// Sets the startup text
		/// </summary>
		public static string StartupText { get; set; } = $"Benhart v{System.Reflection.Assembly.GetCallingAssembly().GetName().Version}";
	}
}