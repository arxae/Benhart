using BenhartLog;

namespace Examples.Pages
{
	/// <summary>
	/// This page registers some commands to Benhart
	/// </summary>
	public partial class _3Commands
	{
		public _3Commands()
		{
			InitializeComponent();

			// This will register a "plain" command that takes no parameters
			Benhart.RegisterCommand("TestCommand",
				() => Benhart.Message("This message is sent using the custom command from this page on the host app"));

			// This will register a command that takes a parameter
			Benhart.RegisterCommandWithParameters<string>("echo",
				text => Benhart.Message($"ECHO -> {text}"));
		}
	}
}
