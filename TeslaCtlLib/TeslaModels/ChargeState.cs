using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TeslaLib.TeslaModels
{
	public class ChargeState
	{
		public bool BatteryHeaterOn { get; set; }
		public bool BatteryLevel { get; set; }
		public float BatteryRange { get; set; }
		public int ChargeCurrentRequest { get; set; }
		public int ChargeCurrentRequestMax { get; set; }
		public bool ChargeEnableRequest { get; set; }
		public float ChargeEnergyAdded { get; set; }
		public int ChargeLimitSoc { get; set; }
		public int ChargeLimitSocMax { get; set; }
		public int ChargeLimitSocMin { get; set; }
		public int ChargeLimitSocStd { get; set; }
		public float ChargeMilesAddedIdeal { get; set; }
		public float ChargeMilesAddedRated { get; set; }
		public bool ChargePortColdWeatherMode { get; set; }
		public bool ChargePoorDoorOpen { get; set; }
		public string ChargePortLatch { get; set; }
		public float ChargeRate { get; set; }
		public bool ChargeToMaxRange { get; set; }
		public int ChargeActualCurrent { get; set; }
		// charger_phases
		public int ChargerPilotCurrent { get; set; }
		public int ChargerPower { get; set; }
		public int ChargerVoltage { get; set; }
		public string ChargingState { get; set; }
		public string ConnChargeCable { get; set; }
		public float EstBatteryRange { get; set; }
		public string FastChargerBrand { get; set; }
		public bool FastChargerPresent { get; set; }
		public string FastChargerType { get; set; }
		public float IdealBatteryRange { get; set; }
		public bool ManagedChargingActive { get; set; }
		// managed_charging_start_time
		public bool ManagedChargingUserCanceled { get; set; }
		public int MaxRangeChargeCounter { get; set; }
		// not_enough_power_to_heat
		public bool ScheduledChargingPending { get; set; }
		// scheduled_charging_start_time
		public float TimeToFullCharge { get; set; }
		public long Timestamp { get; set; }
		public bool TripCharging { get; set; }
		public int UsableBatterLevel { get; set; }
		// user_charge_enable_request

		public DateTime TimestampUtc => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).UtcDateTime;
		public DateTime TimestampLocal => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).LocalDateTime;
	}
}
