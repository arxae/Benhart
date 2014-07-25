using System;
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

		public BenhartLogWindow()
		{
			InitializeComponent();
		}

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
	}
}
