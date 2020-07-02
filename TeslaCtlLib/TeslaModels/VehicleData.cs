using System;
using System.Collections.Generic;
using System.Text;

namespace TeslaLib.TeslaModels
{
	public class VehicleData
	{
		public long Id { get; set; }
		public int UserId { get; set; }
		public long VehicleId { get; set; }
		public string VIN { get; set; }
		public string DisplayName { get; set; }
		public string OptionsCodes { get; set; }
		public string Color { get; set; }
		public string[] Tokens { get; set; }
		public string State { get; set; }
		public bool InService { get; set; }
		public bool CalendarEnabled { get; set; }
		public int ApiVersion { get; set; }
		// backseat_token
		// backseat_token_updated_at
		public ChargeState ChargeState { get; set; }
		public ClimateState ClimateState { get; set; }
		public DriveState DriveState { get; set; }
		public GuiSettings GuiSettings { get; set; }
		public VehicleConfig VehicleConfig { get; set; }
		public VehicleState VehicleState { get; set; }
	}
}
