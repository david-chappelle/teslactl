using System;
using System.Collections.Generic;
using System.Text;

namespace TeslaLib.TeslaModels
{
	public class NearbyChargingSites
	{
		public long CongestionSyncTimeUtcSecs { get; set; }
		public ChargingSite[] DestinationCharging { get; set; }
		public ChargingSite[] Superchargers { get; set; }
		public long Timestamp { get; set; }

		public DateTime TimestampUtc => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).UtcDateTime;
		public DateTime TimestampLocal => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).LocalDateTime;
	}
}
