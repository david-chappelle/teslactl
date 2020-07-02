using System;
using Newtonsoft.Json;

namespace TeslaLib.TeslaModels
{
	public class GuiSettings
	{
		public bool Gui24HourTime { get; set; }
		public string GuiChargeRateUnits { get; set; }
		public string GuiDistanceUnits { get; set; }
		public string GuiRangeDisplay { get; set; }
		public string GuiTemperatureUnits { get; set; }
		public bool ShowRangeUnits { get; set; }
		public long Timestamp { get; set; }

		public DateTime TimestampUtc => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).UtcDateTime;
		public DateTime TimestampLocal => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).LocalDateTime;
	}
}
