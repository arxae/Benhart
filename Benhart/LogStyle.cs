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

		/// <summary>
		/// Gets a logstyle based on severity
		/// </summary>
		/// <param name="severity"></param>
		/// <returns></returns>
		public static LogStyle GetLogStyle(LogLevel severity)
		{
			switch (severity)
			{
				case LogLevel.Message: return Benhart.Styles["LogMessage"];
				case LogLevel.Debug: return Benhart.Styles["LogDebug"];
				case LogLevel.Info: return Benhart.Styles["LogInfo"];
				case LogLevel.Warning: return Benhart.Styles["LogWarning"];
				case LogLevel.Error: return Benhart.Styles["LogError"];
			}

			return GetDefaultStyle();
		}

		/// <summary>
		/// Returns the default style
		/// </summary>
		/// <returns></returns>
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