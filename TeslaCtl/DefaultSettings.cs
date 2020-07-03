using System.IO;
using Newtonsoft.Json;

namespace TeslaCtl
{
	internal class DefaultSettings
	{
		public string AccessToken { get; set; }
		public string VehicleId { get; set; }

		public static DefaultSettings ReadFromFile()
		{
			var defaultSettingsFile = getDefaultSettingsFile();
			if (!File.Exists(defaultSettingsFile))
				return new DefaultSettings();

			var contents = File.ReadAllText(defaultSettingsFile);
			return JsonConvert.DeserializeObject<DefaultSettings>(contents);
		}

		public void UpdateFile()
		{
			var contents = getDefaultSettingsFile();
			var defaultSettingsFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "defaults.json");
			File.WriteAllText(defaultSettingsFile, contents);
		}

		public static void RemoveFile()
		{
			var defaultSettingsFile = getDefaultSettingsFile();
			File.Delete(defaultSettingsFile);
		}

		private static string getDefaultSettingsFile()
		{
			return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "defaults.json");
		}
	}
}
