using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace BenhartLog.Windows
{
	public partial class BenhartLogWindow
	{
		internal bool AllowExit = false;

		public BenhartLogWindow()
		{
			InitializeComponent();

			// Register commands
			Benhart.RegisterCommandWithParameters<string>("debug", Benhart.Debug);
			Benhart.RegisterCommandWithParameters<string>("error", Benhart.Error);
			Benhart.RegisterCommandWithParameters<string>("info", Benhart.Info);
			Benhart.RegisterCommandWithParameters<string>("message", Benhart.Message);
			Benhart.RegisterCommandWithParameters<string>("warning", Benhart.Warning);
			Benhart.RegisterCommand("hide", Benhart.Hide);

			var assembly = Assembly.GetCallingAssembly().GetName();
			Benhart.RegisterCommand("version", () =>
			{
				Benhart.Info($"Bernhart Logging Window v{assembly.Version}");
			});
			Benhart.RegisterCommand("cmdlist",
				() => Benhart.Info($"The following commands are available: {string.Join(", ", Benhart.GetAllCommands())}"));

			Benhart.RegisterCommand("dump",
				() =>
				{
					var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
					var json = serializer.Serialize(new[]
					{
						new
						{
							Watches = Benhart.LogPage._watches,
							LogText = Benhart.LogPage.GetLogText()
						}
					});

					Benhart.Message(json);
					Clipboard.SetText(json);
					Benhart.Info("The contents of the dump have been copied to your clipboard.");
				});

			// TODO: Temporary fix because the XAML event threw errors
			Closing += Window_Closing;

			// Set the correct logpage and load it into the correct frame
			Benhart.LogPage = new Pages.MainLogWindowPage();
			MainTabFrame.Content = Benhart.LogPage;
			MainTab.Visibility = Visibility.Collapsed;
		}

		// Prevent the window itself from closing unless the propper signal has been given
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !AllowExit;

			if (AllowExit == false)
			{
				Hide();
			}
		}

		public void AddPage(string pageName)
		{
			var page = Benhart.CustomPages[pageName];
			var tab = new TabItem { Header = pageName };
			var frame = new Frame { Content = page };

			tab.Content = frame;
			TabControl.Items.Add(tab);

			if (MainTab.Visibility == Visibility.Collapsed)
			{
				MainTab.Visibility = Visibility.Visible;
			}
		}
	}
}
