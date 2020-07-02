namespace TeslaLib.TeslaModels
{
	public class ChargingSite
	{
		public Location Location { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public float DistanceMiles { get; set; }
		public int? AvailableStalls { get; set; }
		public int? TotalStalls { get; set; }
		public bool? SiteClosed { get; set; }
	}
}
