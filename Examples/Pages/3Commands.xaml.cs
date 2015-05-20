using System.Windows.Controls;
using BenhartLog;

namespace Examples.Pages
{
	/// <summary>
	/// Interaction logic for _3Commands.xaml
	/// </summary>
	public partial class _3Commands : Page
	{
		public _3Commands()
		{
			InitializeComponent();

			Benhart.RegisterCommand("TestCommand",
				() => Benhart.Message("This message is sent using the custom command from this page on the host app"));

			Benhart.RegisterCommandWithParameters<string>("echo",
				text => Benhart.Message($"ECHO -> {text}"));
		}
	}
}
