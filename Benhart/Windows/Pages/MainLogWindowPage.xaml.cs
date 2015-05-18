using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace BenhartLog.Windows.Pages
{
	public partial class MainLogWindowPage
	{
		public bool ShowTime = true;
		public string TimeFormat = "HH:mm:ss";
		public string TimeMessageSepparator = " | ";

		private readonly ObservableCollection<WatchEntry> _watches;

		public MainLogWindowPage()
		{
			InitializeComponent();
			Util.AddDefaultStyles();

			_watches = new ObservableCollection<WatchEntry>();
			WatchGrid.ItemsSource = _watches;
		}

		public void WriteStyledMessage(string text, LogStyle style)
		{
			if (ShowTime)
			{
				text = DateTime.Now.ToString(TimeFormat) + TimeMessageSepparator + text;
			}

			var range = new TextRange(LogTextBox.Document.ContentEnd, LogTextBox.Document.ContentEnd) { Text = text + Environment.NewLine };

			if (style.ForegroundColor != null) range.ApplyPropertyValue(TextElement.ForegroundProperty, style.ForegroundColor);
			if (style.BackgroundColor != null) range.ApplyPropertyValue(TextElement.BackgroundProperty, style.BackgroundColor);
			if (style.IsBold) range.ApplyPropertyValue(TextElement.FontWeightProperty, "Bold");
			if (style.IsItalic) range.ApplyPropertyValue(TextElement.FontStyleProperty, "Italic");

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

		public void SetWatch(string name, object value)
		{
			var item = _watches.FirstOrDefault(i => i.WatchName == name);

			if (item == null)
			{
				_watches.Add(new WatchEntry(name, value.ToString()));
			}
			else
			{
				item.WatchValue = value.ToString();
				WatchGrid.ItemsSource = null;
				WatchGrid.ItemsSource = _watches;
			}
		}

		private void ConsoleInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				ParseCommand();
			}
		}

		private void ConsoleInputButton_Click(object sender, RoutedEventArgs e)
		{
			ParseCommand();
			ConsoleInput.Clear();
		}

		private void ParseCommand()
		{
			var parts = ConsoleInput.Text.Split(' ').ToList();
			var command = parts[0];
			parts.Remove(command);

			if (CommandManager.RunCommand(command, string.Join(" ", parts)) == -1)
			{
				Benhart.Error($"The command \"{command}\" is not registered.");
			}

			ConsoleInput.Clear();
		}
	}
}
