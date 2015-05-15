using System.Windows.Media;

namespace BenhartLog
{
	public class LogStyle
	{
		public SolidColorBrush ForegroundColor;
		public SolidColorBrush BackgroundColor;
		public bool IsBold;
		public bool IsItalic;
		public LineStyle LineStyle;

		public static LogStyle GetLogStyle(LogLevel severity)
		{
			switch (severity)
			{
				case LogLevel.Message: return Benhart.Styles["LogMessage"];
				case LogLevel.Debug: return Benhart.Styles["LogMessage"];
				case LogLevel.Info: return Benhart.Styles["LogMessage"];
				case LogLevel.Warning: return Benhart.Styles["LogMessage"];
				case LogLevel.Error: return Benhart.Styles["LogMessage"];
			}

			return GetDefaultStyle();
		}

		public static LogStyle GetDefaultStyle()
		{
			return new LogStyle
			{
				BackgroundColor = Brushes.Transparent,
				ForegroundColor = Brushes.Black,
				IsBold = false,
				IsItalic = false,
				LineStyle = LineStyle.None
			};
		}
	}
}