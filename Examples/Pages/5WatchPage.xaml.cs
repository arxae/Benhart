using System.Windows;
using System.Windows.Controls;
using BenhartLog;

namespace Examples.Pages
{
	/// <summary>
	/// Interaction logic for _5WatchPage.xaml
	/// </summary>
	public partial class _5WatchPage : Page
	{
		public _5WatchPage()
		{
			InitializeComponent();
		}

		private void BtnSetUpdateWatch_Click(object sender, RoutedEventArgs e)
		{
			Benhart.SetWatch(TxtWatchName.Text, TxtWatchValue.Text);
		}
	}
}
