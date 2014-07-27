namespace BenhartLog
{
	public class WatchEntry
	{
		public string WatchName { get; set; }
		public string WatchValue { get; set; }

		public WatchEntry(string name, string value)
		{
			WatchName = name;
			WatchValue = value;
		}
	}
}
