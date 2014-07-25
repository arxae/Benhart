using System;
using System.Windows.Media;

namespace BenhartLog
{
	public class LogStyle
	{
		public SolidColorBrush ForegroundColor;
		public SolidColorBrush BackgroundColor;
		public bool Bold;
		public bool Italic;
		public LineStyle LineStyle;

		/// <summary>
		/// Creates a default style based uppon the severity level
		/// </summary>
		/// <param name="severity"></param>
		/// <returns></returns>
		public static LogStyle GetLogStyle(LogLevel severity)
		{
			var style = new LogStyle();
			switch (severity)
			{
				case LogLevel.Message:
					style.Bold = true;
					style.ForegroundColor = Brushes.Black;
					break;
				case LogLevel.Debug:
					style.Bold = true;
					style.ForegroundColor = Brushes.Green;
					break;
				case LogLevel.Info:
					style.Bold = true;
					style.ForegroundColor = Brushes.Blue;
					break;
				case LogLevel.Warning:
					style.Bold = true;
					style.ForegroundColor = Brushes.Orange;
					break;
				case LogLevel.Error:
					style.Bold = true;
					style.ForegroundColor = Brushes.Red;
					break;
			}

			return style;
		}
	}
}