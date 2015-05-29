using System.Windows;
using System.Windows.Controls;
using BenhartLog;

namespace Examples
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();

			// Change the startup text and then show the window.
			// If you don't change the startup text BEFORE showing the window, the default will be printed
			BenhartWindowOptions.StartupText = "Benhart Example Application";
			Benhart.AttachTo(this);
			// You can also use Benhart.Show() to just show the window witouth attaching it
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var btn = (Button)sender;

			switch (btn.Name)
			{
				case "BtnAddCustomPage":
					// This will add a custom pagetab to the Benhart window
					Benhart.AddPage("Custom Page Test", new Pages._2CustomPagesPage());
					break;
			}
		}
	}
}
