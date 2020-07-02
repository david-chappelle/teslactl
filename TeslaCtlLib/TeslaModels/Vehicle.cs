namespace TeslaLib.TeslaModels
{
	public class Vehicle
	{
		public long Id { get; set; }
		public long VehicleId { get; set; }
		public string VIN { get; set; }
		public string DisplayName { get; set; }
		public string OptionCodes { get; set; }
		public string Color { get; set; }
		public string[] Tokens { get; set; }
		public string State { get; set; }
		public bool InService { get; set; }
		public bool CalendarEnabled { get; set; }
		public int ApiVersion { get; set; }
	}
}
