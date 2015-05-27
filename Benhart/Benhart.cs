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
		/// <summary>
		/// Show the window
		/// </summary>
		public static void Show()
		{
			Window.Show();
		}

		/// <summary>
		/// Hide the window
		/// </summary>
		public static void Hide()
		{
			Window.Hide();
		}

		/// <summary>
		/// Set the position of the window
		/// </summary>
		/// <param name="left"></param>
		/// <param name="top"></param>
		public static void SetPosition(int left, int top)
		{
			Window.Left = left;
			Window.Top = top;
		}

		/// <summary>
		/// Attaches to the specified window. Logwindow will attach itself to the right side of the parent window and follow it around
		/// </summary>
		/// <param name="win">Designate parent window</param>
		/// <param name="showWindowOnAttach">Should the window be opened when it is attached?</param>
		public static void AttachTo(Window win, bool showWindowOnAttach = true)
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

			if (showWindowOnAttach)
			{
				Window.Show();
			}
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
		/// <summary>
		/// Write to the log with a custom style
		/// </summary>
		/// <param name="text"></param>
		/// <param name="style"></param>
		public static void Log(LogStyle style, string text)
		{
			LogPage.WriteStyledMessage(text, style);
		}

		/// <summary>
		/// Write to the log with a style stored in the style dictionary
		/// </summary>
		/// <param name="text"></param>
		/// <param name="styleName"></param>
		public static void Log(string styleName, string text)
		{
			LogPage.WriteStyledMessage(text, GetLogStyle(styleName));
		}

		/// <summary>
		/// Write to the log with a specified severity level
		/// </summary>
		/// <param name="text"></param>
		/// <param name="severity"></param>
		public static void Log(LogLevel severity, string text)
		{
			LogPage.WriteStyledMessage(text, LogStyle.GetLogStyle(severity));
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



		// ===		PAGE MANAGEMENT	===
		/// <summary>
		/// Add a page with the specified pagename and WPF Page object.
		/// </summary>
		/// <param name="pageName">The name to recall the page later and also the name of the tab</param>
		/// <param name="pageItem"></param>
		public static void AddPage(string pageName, Page pageItem)
		{
			if (CustomPages.ContainsKey(pageName)) return;

			CustomPages.Add(pageName, pageItem);
			Window.AddPage(pageName);
		}

		/// <summary>
		/// Gets the custom added page
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="pageName"></param>
		/// <param name="throwExceptionOnFail"></param>
		/// <returns></returns>
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

		// ===		COMMAND MANAGEMENT	===
		/// <summary>
		/// Registers a command witouth parameters
		/// </summary>
		/// <param name="command">The command that has to be inputted into the console for it to be called</param>
		/// <param name="commandAction">An action that gets executed when the command is ran</param>
		public static void RegisterCommand(string command, Action commandAction)
		{
			CommandManager.RegisterCommand(command, commandAction);
		}

		/// <summary>
		/// Registers a command with parameters
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="command">The command that has to be inputted into the console for it to be called</param>
		/// <param name="commandAction">An action that gets executed when the command is ran</param>
		public static void RegisterCommandWithParameters<T>(string command, Action<T> commandAction)
		{
			CommandManager.RegisterCommandWithParameters(command, commandAction);
		}

		/// <summary>
		/// Removes a parameterless command
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public static bool UnregisterCommand(string command)
		{
			return CommandManager.UnregisterCommand(command);
		}

		/// <summary>
		/// Removes a command with parameters
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public static bool UnregisterCommandWithParamters(string command)
		{
			return CommandManager.UnregisterCommandWithParamters(command);
		}

		/// <summary>
		/// Returns an array of all the usable commands
		/// </summary>
		/// <returns></returns>
		public static string[] GetAllCommands()
		{
			return CommandManager.ReportCommands();
		}
	}
}
