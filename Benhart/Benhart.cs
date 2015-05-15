using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BenhartLog.Windows;
using BenhartLog.Windows.Pages;

namespace BenhartLog
{
	public static class Benhart
	{
		private static BenhartLogWindow _logWindowInstance;
		internal static BenhartLogWindow Window => _logWindowInstance ?? (_logWindowInstance = new BenhartLogWindow());

		internal static MainLogWindowPage LogPage { get; set; }

		private static Dictionary<string, LogStyle> _dicStylesInstance;
		public static Dictionary<string, LogStyle> Styles => _dicStylesInstance ?? (_dicStylesInstance = new Dictionary<string, LogStyle>());

		private static Dictionary<string, Page> _dicCustomPagesInstance;
		internal static Dictionary<string, Page> CustomPages => _dicCustomPagesInstance ?? (_dicCustomPagesInstance = new Dictionary<string, Page>());

		private static Window _parentWindow;
		private static bool _isAttached;

		// ===		WINDOW MANAGEMENT	===
		public static void Show()
		{
			Window.Show();
		}

		public static void Hide()
		{
			Window.Hide();
		}

		public static void SetPosition(int left, int top)
		{
			Window.Left = left;
			Window.Top = top;
		}

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
				if (_isAttached)
				{
					Window.AllowExit = true;
					Window.Close();
				}
				else
				{
					_parentWindow = null;
				}
			};

			_isAttached = true;
			UpdatePosition();
			Window.Show();
		}

		/// <summary>
		/// When the window is detached from the parent, reattach the window
		/// </summary>
		public static void Attach()
		{
			if (_parentWindow == null) return;
			_isAttached = true;
			UpdatePosition();
		}

		/// <summary>
		/// Detach the window from the parent
		/// </summary>
		public static void Detach()
		{
			_isAttached = false;
		}

		/// <summary>
		/// Updates the position in correspondance to the parent.
		/// </summary>
		private static void UpdatePosition()
		{
			if (_isAttached == false) return;
			Window.Left = _parentWindow.Left + _parentWindow.Width;
			Window.Top = _parentWindow.Top;
		}

		// ===		LOG WRITING			===
		public static void Log(string text, LogStyle style)
		{
			LogPage.WriteStyledMessage(text, style);
		}

		public static void Log(string text, string styleName)
		{
			LogPage.WriteStyledMessage(text, GetLogStyle(styleName));
		}

		// Quick log methods
		/// <summary>
		/// Writes a Debug message to the log
		/// </summary>
		/// <param name="text"></param>
		public static void Debug(string text)
		{
			LogPage.WriteStyledMessage(text, LogStyle.GetLogStyle(LogLevel.Debug));
		}

		/// <summary>
		/// Writes a Message message to the log
		/// </summary>
		/// <param name="text"></param>
		public static void Message(string text)
		{
			LogPage.WriteStyledMessage(text, LogStyle.GetLogStyle(LogLevel.Message));
		}

		/// <summary>
		/// Writes a Info message to the log
		/// </summary>
		/// <param name="text"></param>
		public static void Info(string text)
		{
			LogPage.WriteStyledMessage(text, LogStyle.GetLogStyle(LogLevel.Info));
		}

		/// <summary>
		/// Writes a Warning message to the log
		/// </summary>
		/// <param name="text"></param>
		public static void Warning(string text)
		{
			LogPage.WriteStyledMessage(text, LogStyle.GetLogStyle(LogLevel.Warning));
		}

		/// <summary>
		/// Writes a Error message to the log
		/// </summary>
		/// <param name="text"></param>
		public static void Error(string text)
		{
			LogPage.WriteStyledMessage(text, LogStyle.GetLogStyle(LogLevel.Error));
		}

		// ===		WATCHES				===
		/// <summary>
		/// Sets a watch value. If the watchname already exists, then the value will be updated
		/// </summary>
		/// <param name="watchName"></param>
		/// <param name="watchValue"></param>
		public static void SetWatch(string watchName, object watchValue)
		{
			LogPage.SetWatch(watchName, watchValue);
		}

		// ===		STYLE MANAGEMENT	===
		/// <summary>
		/// Gets a specific logstyle from the styles dictionary
		/// </summary>
		/// <param name="styleName">The name of the style</param>
		/// <param name="throwExceptionOnFail">If the style can't be find, throw an exception?</param>
		/// <returns>The style corresponding to styleName. If the style cannot be found, a default style will be returned</returns>
		public static LogStyle GetLogStyle(string styleName, bool throwExceptionOnFail = false)
		{
			if (Styles.ContainsKey(styleName))
			{
				return Styles[styleName];
			}

			if (throwExceptionOnFail)
			{
				throw new Exception($"The style \"{styleName}\" was not found.");
			}

			return LogStyle.GetDefaultStyle();
		}

		/// <summary>
		/// Add a style to the style dictionaru
		/// </summary>
		/// <param name="styleName">The name of the style</param>
		/// <param name="style">A LogStyle object conaining the style information</param>
		/// <param name="overwriteIfExists">If the style already exists, replace it?</param>
		/// <param name="throwExceptionOnFail">If the style aready exists, throw an exception (If overwriteIfExists is set to true, this will be ignored)</param>
		public static void AddLogStyle(string styleName, LogStyle style, bool overwriteIfExists = false, bool throwExceptionOnFail = false)
		{
			bool exists = Styles.ContainsKey(styleName);

			if (exists && overwriteIfExists)
			{
				Styles.Remove(styleName);
			}

			if (exists && !overwriteIfExists && throwExceptionOnFail)
			{
				throw new Exception($"The style \"{styleName}\" already exists.");
			}

			Styles.Add(styleName, style);
		}



		// // // // // //
		// TEST AREA   //
		// // // // // //
		public static void AddPage(string pageName, Page pageItem)
		{
			if (CustomPages.ContainsKey(pageName)) return;

			CustomPages.Add(pageName, pageItem);
			Window.AddPage(pageName);
		}

		public static T GetPage<T>(string pageName, bool throwExceptionOnFail = false) where T : Page
		{
			if (CustomPages.ContainsKey(pageName))
			{
				return (T)CustomPages[pageName];
			}

			if (throwExceptionOnFail)
			{
				throw new Exception($"Page {pageName} was not found.");
			}

			return null;
		}
	}
}
