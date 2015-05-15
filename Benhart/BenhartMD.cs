using System.Collections.Generic;
using MarkdownLog;

namespace BenhartLog
{
	/// <summary>
	/// Outputs Markdown instead of regular text
	/// Uses MarkdownLog by Wheelies: https://github.com/Wheelies/MarkdownLog
	/// </summary>
	public class BenhartMD
	{
		/// <summary>
		/// Outputs an IEnumerable list as a bulleted list
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="severity"></param>
		/// <param name="enumerable"></param>
		/// <param name="title">The title that is used describing the list</param>
		public static void BulletList<T>(LogLevel severity, IEnumerable<T> enumerable, string title = "List: ")
		{
			Benhart.Log(severity, title);
			bool showTimestamp = BenhartWindowOptions.ShowTimestamp;
			BenhartWindowOptions.ShowTimestamp = false;
			Benhart.Log(severity, enumerable.ToMarkdownBulletedList().ToString());
			BenhartWindowOptions.ShowTimestamp = showTimestamp;
		}

		/// <summary>
		/// Outputs an IEnumerable list as a bulleted list
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="severity"></param>
		/// <param name="enumerable"></param>
		/// <param name="title">The title that is used describing the list</param>
		public static void NumberedList<T>(LogLevel severity, IEnumerable<T> enumerable, string title = "List: ")
		{
			Benhart.Log(severity, title);
			bool showTimeValue = BenhartWindowOptions.ShowTimestamp;
			BenhartWindowOptions.ShowTimestamp = false;
			Benhart.Log(severity, enumerable.ToMarkdownNumberedList().ToString());
			BenhartWindowOptions.ShowTimestamp = showTimeValue;
		}

		/// <summary>
		/// Outputs an anonymous IEnumerable object as a table
		/// </summary>
		/// <example>
		/// Example input object:
		/// <code>
		/// var data = new[]
		/// {
		///		new{Year = 1991, Album = "Out of Time", Songs=11, Rating = "* * * *"},
		///		new{Year = 1992, Album = "Automatic for the People", Songs=12, Rating = "* * * * *"},
		///		new{Year = 1994, Album = "Monster", Songs=12, Rating = "* * *"}
		/// };
		/// </code>
		/// </example>
		/// <typeparam name="T"></typeparam>
		/// <param name="severity"></param>
		/// <param name="enumerable"></param>
		/// <param name="title">The title that is used describing the list</param>
		public static void Table<T>(LogLevel severity, IEnumerable<T> enumerable, string title = "Table: ")
		{
			Benhart.Log(severity, title);
			bool showTimeValue = BenhartWindowOptions.ShowTimestamp;
			BenhartWindowOptions.ShowTimestamp = false;
			Benhart.Log(severity, enumerable.ToMarkdownTable().ToString());
			BenhartWindowOptions.ShowTimestamp = showTimeValue;
		}

		/// <summary>
		/// Outputs a piece of text as a markdown paragraph
		/// </summary>
		/// <param name="severity"></param>
		/// <param name="text"></param>
		/// <param name="title"></param>
		public static void Paragraph(LogLevel severity, string text, string title = "")
		{
			Benhart.Log(severity, title);
			bool showTimeValue = BenhartWindowOptions.ShowTimestamp;
			BenhartWindowOptions.ShowTimestamp = false;
			Benhart.Log(severity, text.ToMarkdownParagraph().ToString());
			BenhartWindowOptions.ShowTimestamp = showTimeValue;
		}

		//TODO: Do later (output html)
		//private static string Output<T>(T markdown, bool asHtml) where T : BulletedList
		//{
		//	if (asHtml)
		//	{
		//		return markdown.ToHtml();
		//	}

		//	return markdown.ToString();
		//}
	}
}
