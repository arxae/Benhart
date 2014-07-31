using System.Collections.Generic;
using BenhartLog.MarkdownLog;

namespace BenhartLog
{
	/// <summary>
	/// Outputs Markdown instead of regular text
	/// Uses MarkdownLog by Wheelies: https://github.com/Wheelies/MarkdownLog
	/// </summary>
	/// TODO: less code repetition but make sure the value is updated after first call 
	public static class BenhartMarkdown
	{
		public static void Table<T>(LogLevel severity, IEnumerable<T> enumerable, string title = "Table: ")
		{
			Benhart.Log(severity, title);

			bool showTimeValue = Benhart.ShowTime;

			Benhart.ShowTime = false;

			Benhart.Log(severity, enumerable.ToMarkdownTable().ToString());

			Benhart.ShowTime = showTimeValue;
		}

		public static void NumberedList<T>(LogLevel severity, IEnumerable<T> enumerable, string title = "List: ")
		{
			Benhart.Log(severity, title);

			bool showTimeValue = Benhart.ShowTime;

			Benhart.ShowTime = false;

			Benhart.Log(severity, enumerable.ToMarkdownNumberedList().ToString());

			Benhart.ShowTime = showTimeValue;
		}

		public static void BulletList<T>(LogLevel severity, IEnumerable<T> enumerable, string title = "List: ")
		{
			Benhart.Log(severity, title);

			bool showTimeValue = Benhart.ShowTime;

			Benhart.ShowTime = false;

			Benhart.Log(severity, enumerable.ToMarkdownBulletedList().ToString());

			Benhart.ShowTime = showTimeValue;
		}

		public static void Paragraph(LogLevel severity, string text, string title = "")
		{
			Benhart.Log(severity, title);

			bool showTimeValue = Benhart.ShowTime;

			Benhart.ShowTime = false;

			Benhart.Log(severity, text.ToMarkdownParagraph().ToString());

			Benhart.ShowTime = showTimeValue;
		}
	}
}
