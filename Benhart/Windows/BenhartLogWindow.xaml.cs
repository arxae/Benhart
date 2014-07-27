using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace BenhartLog.Windows
{
	/// <summary>
	/// Interaction logic for LogWindow.xaml
	/// </summary>
	public partial class BenhartLogWindow
	{
		public bool ShowTime = true;
		public string TimeFormat = "HH:mm:ss";
		public string TimeMessageSepparator = " | ";

		private ObservableCollection<WatchEntry> Watches;

		public BenhartLogWindow()
		{
			InitializeComponent();

			Watches = new ObservableCollection<WatchEntry>();
			WatchGrid.ItemsSource = Watches;
		}

		/// <summary>
		/// Displays a styled message in the logwinow
		/// </summary>
		/// <param name="text"></param>
		/// <param name="style"></param>
		public void StyledMessage(string text, LogStyle style)
		{
			if (ShowTime)
			{
				text = DateTime.Now.ToString(TimeFormat) + TimeMessageSepparator + text;
			}

			var range = new TextRange(LogTextBox.Document.ContentEnd, LogTextBox.Document.ContentEnd) { Text = text + Environment.NewLine };

			if (style.ForegroundColor != null) range.ApplyPropertyValue(TextElement.ForegroundProperty, style.ForegroundColor);
			if (style.BackgroundColor != null) range.ApplyPropertyValue(TextElement.BackgroundProperty, style.BackgroundColor);
			if (style.Bold) range.ApplyPropertyValue(TextElement.FontWeightProperty, "Bold");
			if (style.Italic) range.ApplyPropertyValue(TextElement.FontStyleProperty, "Italic");

			if (style.LineStyle != LineStyle.None)
			{
				switch (style.LineStyle)
				{
					case LineStyle.Overline:
						range.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.OverLine);
						break;
					case LineStyle.Striketrough:
						range.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);
						break;
					case LineStyle.Underline:
						range.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
						break;
				}
			}

			LogTextBox.ScrollToEnd();
		}

		/// <summary>
		/// Sets a value in the watch window
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void SetWatch(string name, string value)
		{
			var item = Watches.FirstOrDefault(i => i.WatchName == name);

			if (item == null)
			{
				Watches.Add(new WatchEntry(name, value));
			}
			else
			{
				item.WatchValue = value;
				WatchGrid.ItemsSource = null;
				WatchGrid.ItemsSource = Watches;
			}
		}
	}
}
