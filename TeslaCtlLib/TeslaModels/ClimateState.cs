using System;

namespace TeslaLib.TeslaModels
{
	public partial class ClimateState
	{
		public bool BatteryHeater { get; set; }
		// battery_heater-no_power
		public string ClimateKeeperMode { get; set; }
		public int DefrostMode { get; set; }
		public float DriverTempSetting { get; set; }
		public int FanStatus { get; set; }
		public float InsideTemp { get; set; }
		public bool IsAutoConditioningOn { get; set; }
		public bool IsClimateOn { get; set; }
		public bool IsFrontDefrosterOn { get; set; }
		public bool IsPreconditioning { get; set; }
		public bool IsRearDefrosterOn { get; set; }
		public int LeftTempDirection { get; set; }
		public float MaxAvailTemp { get; set; }
		public float MinAvailTemp { get; set; }
		public float OutsideTemp { get; set; }
		public float PassengerTempSetting { get; set; }
		public bool RemoteHeaterControlEnabled { get; set; }
		public int RightTempDirection { get; set; }
		public int SeatHeaterLeft { get; set; }
		public int SeatHeaterRearCenter { get; set; }
		public int SeatHeaterRearLeft { get; set; }
		public int SeatHeaterRearRight { get; set; }
		public int SeatHeaterRight { get; set; }
		public bool SideMirrorHeaters { get; set; }
		public long Timestamp { get; set; }
		public bool WiperBladeHeater { get; set; }

		public DateTime TimestampUtc => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).UtcDateTime;
		public DateTime TimestampLocal => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).LocalDateTime;
		public float InsideTempF => TempConvert.ToFahrenheit(InsideTemp);
		public float OutsideTempF => TempConvert.ToFahrenheit(OutsideTemp);
		public float DriverTempSettingF => TempConvert.ToFahrenheit(DriverTempSetting);
		public float PassengerTempSettingF => TempConvert.ToFahrenheit(PassengerTempSetting);
		public float MinAvailTempF => TempConvert.ToFahrenheit(MinAvailTemp);
		public float MaxAvailTempF => TempConvert.ToFahrenheit(MaxAvailTemp);
	}
}
