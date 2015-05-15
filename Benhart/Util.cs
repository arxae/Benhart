using System.Windows.Media;

namespace BenhartLog
{
	public static class Util
	{
		/// <summary>
		/// Adds all the default styles to the styles list
		/// </summary>
		public static void AddDefaultStyles()
		{
			Benhart.AddLogStyle("LogMessage", new LogStyle
			{
				IsBold = true,
				ForegroundColor = Brushes.Black
			});

			Benhart.AddLogStyle("LogDebug", new LogStyle
			{
				IsBold = true,
				ForegroundColor = Brushes.Green
			});

			Benhart.AddLogStyle("LogInfo", new LogStyle
			{
				IsBold = true,
				ForegroundColor = Brushes.Blue
			});

			Benhart.AddLogStyle("LogWarning", new LogStyle
			{
				IsBold = true,
				IsItalic = true,
				ForegroundColor = Brushes.Orange
			});

			Benhart.AddLogStyle("LogError", new LogStyle
			{
				IsBold = true,
				ForegroundColor = Brushes.Red
			});
		}
	}
}