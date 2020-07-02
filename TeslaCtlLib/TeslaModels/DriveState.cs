using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TeslaLib.TeslaModels
{
	public class DriveState
	{
		public long GpsAsOf { get; set; }
		public int Heading { get; set; }
		public float Latitude { get; set; }
		public float Longitude { get; set; }
		public float NativeLatitude { get; set; }
		public int NativeLocationSupported { get; set; }
		public float NativeLongitude { get; set; }
		public string NativeType { get; set; }
		public int Power { get; set; }
		// shift_state
		// speed
		public long Timestamp { get; set; }

		public DateTime TimestampUtc => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).UtcDateTime;
		public DateTime TimestampLocal => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).LocalDateTime;
		public DateTime GpsAsOfUtc => DateTimeOffset.FromUnixTimeSeconds(GpsAsOf).UtcDateTime;
		public DateTime GpsAsOfLocal => DateTimeOffset.FromUnixTimeSeconds(GpsAsOf).LocalDateTime;
	}
}
