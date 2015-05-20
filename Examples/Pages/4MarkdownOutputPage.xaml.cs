using System.Windows.Controls;
using BenhartLog;

namespace Examples.Pages
{
	/// <summary>
	/// Interaction logic for _4MarkdownOutputPage.xaml
	/// </summary>
	public partial class _4MarkdownOutputPage : Page
	{
		public _4MarkdownOutputPage()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var btn = (Button)sender;
			var planets = new[] { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
			var tableData = new[]
			{
				new{Year = 1991, Album = "Out of Time", Songs=11, Rating = "* * * *"},
				new{Year = 1992, Album = "Automatic for the People", Songs=12, Rating = "* * * * *"},
				new{Year = 1994, Album = "Monster", Songs=12, Rating = "* * *"}
			};

			if (btn.Equals(BtnBulletList))
			{
				BenhartMD.BulletList(LogLevel.Info, planets);
			}
			else if (btn.Equals(BtnNumberedList))
			{
				BenhartMD.NumberedList(LogLevel.Info, planets);
			}
			else if (btn.Equals(BtnTable))
			{
				BenhartMD.Table(LogLevel.Info, tableData);
			}
			else if (btn.Equals(BtnParagraph))
			{
				BenhartMD.Paragraph(LogLevel.Info, "Lolita, light of my life, fire of my loins. My sin, my soul. Lo-lee-ta: the tip of the tongue taking a trip of three steps down the palate to tap, at three, on the teeth. Lo. Lee. Ta.");
			}
		}
	}
}
