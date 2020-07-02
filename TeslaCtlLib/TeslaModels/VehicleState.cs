using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeslaLib.TeslaModels
{
	public class VehicleState
	{
		public int ApiVersion { get; set; }
		public string AutoparkStateV3 { get; set; }
		public string AutoparkStyle { get; set; }
		public bool CalendarSupported { get; set; }
		public string CarVersion { get; set; }
		public int CenterDisplayState { get; set; }
		public int Df { get; set; }
		public int Dr { get; set; }
		public int FdWindow { get; set; }
		public int FpWindow { get; set; }
		public int Ft { get; set; }
		public bool IsUserPresent { get; set; }
		public string LastAutoparkError { get; set; }
		public bool Locked { get; set; }
		public MediaState MediaState { get; set; }
		public bool NotificationsSupported { get; set; }
		public float Odometer { get; set; }
		public bool ParsedCalendarSupported { get; set; }
		public int Pf { get; set; }
		public int Pr { get; set; }
		public int RdWindow { get; set; }
		public bool RemoteStart { get; set; }
		public bool RemotestartEnabled { get; set; }
		public int RpWindow { get; set; }
		public int Rt { get; set; }
		public bool SentryMode { get; set; }
		public bool SentryModeAvailable { get; set; }
		public bool SmartSummonAvailable { get; set; }
		public SoftwareUpdate SoftwareUpdate { get; set; }
		public SpeedLimitMode SpeedLimitMode { get; set; }
		public bool SummonStandbyModeEnabled { get; set; }
		public long Timestamp { get; set; }
		public bool ValetMode { get; set; }
		public bool ValetPinNeeded { get; set; }
		public string VehicleName { get; set; }

		public DateTime TimestampUtc => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).UtcDateTime;
		public DateTime TimestampLocal => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).LocalDateTime;
		public CenterDisplayState? CenterDisplayStateParsed => Enum.IsDefined(typeof(CenterDisplayState), CenterDisplayState) ? (CenterDisplayState?)CenterDisplayState : null;
		public bool FrontTrunkOpen => Ft != 0;
		public bool RearTrunkOpen => Rt != 0;
		public bool DriverFrontOpen => Df != 0;
		public bool PassengerFrontOpen => Pf != 0;
		public bool DriverRearOpen => Dr != 0;
		public bool PassengerRearOpen => Pr != 0;
		public bool FrontDriverWindowOpen => FdWindow != 0;
		public bool FrontPassengerWindowOpen => FpWindow != 0;
		public bool RearDriverWindowOpen => RdWindow != 0;
		public bool RearPassengerWindowOpen => RpWindow != 0;
	}
}
