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
			Benhart.AttachTo(this);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var btn = (Button)sender;

			switch (btn.Name)
			{
				case "BtnAddCustomPage":
					Benhart.AddPage("Custom Page Test", new Pages._2CustomPagesPage());
					break;
			}
		}
	}
}
