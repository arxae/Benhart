using System.Windows;
using BenhartLog;

namespace Examples.Pages
{
	/// <summary>
	/// This page shows how to set watches.
	/// Watches can be updated by setting the watch again using the same name
	/// </summary>
	public partial class _5WatchPage
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
