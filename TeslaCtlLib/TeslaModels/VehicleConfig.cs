using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeslaLib.TeslaModels
{
	public class VehicleConfig
	{
		public bool CanAcceptNavigationRequests { get; set; }
		public bool CanActuateTrunks { get; set; }
		public string CarSpecialType { get; set; }
		public string CarType { get; set; }
		public string ChargePortType { get; set; }
		public bool EuVehicle { get; set; }
		public string ExteriorColor { get; set; }
		public bool HasAirSuspension { get; set; }
		public bool HasLudicrousMode { get; set; }
		public int KeyVersion { get; set; }
		public bool MotorizedChargePort { get; set; }
		public bool Plg { get; set; }
		public int RearSeatHeaters { get; set; }
		// rear_seat_type
		public bool Rhd { get; set; }
		public string RoofColor { get; set; }
		// seat_type
		public string SpoilerType { get; set; }
		// sun_roof_installed
		public string ThirdRowSeats { get; set; }
		public long Timestamp { get; set; }
		public bool UseRangeBadging { get; set; }
		public string WheelType { get; set; }

		public DateTime TimestampUtc => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).UtcDateTime;
		public DateTime TimestampLocal => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).LocalDateTime;
	}
}
