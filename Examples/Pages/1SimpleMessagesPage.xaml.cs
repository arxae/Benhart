using System.Windows;
using System.Windows.Controls;
using BenhartLog;

namespace Examples.Pages
{
	/// <summary>
	/// Interaction logic for _1SimpleMessagesPage.xaml
	/// </summary>
	public partial class _1SimpleMessagesPage
	{
		public _1SimpleMessagesPage()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;

			switch (btn.Name)
			{
				case "BtnMessage":
					Benhart.Message(TxtInput.Text);
					break;
				case "BtnDebug":
					Benhart.Debug(TxtInput.Text);
					break;
				case "BtnInfo":
					Benhart.Info(TxtInput.Text);
					break;
				case "BtnWarning":
					Benhart.Warning(TxtInput.Text);
					break;
				case "BtnError":
					Benhart.Error(TxtInput.Text);
					break;
			}
		}
	}
}
