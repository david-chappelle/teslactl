using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TeslaCtlLib;
using TeslaLib.TeslaModels;

namespace TeslaLib
{
	// Tesla commands
	// https://www.teslaapi.io/vehicles/commands
	// https://tesla-api.timdorr.com/vehicle/commands

	public partial class TeslaClient
	{
		/// <summary>
		/// Request that the vehicle wake up.  The vehicle may be in the process of waking up when the API returns.
		/// </summary>
		/// <returns><see cref="Vehicle"/></returns>
		public async Task<Vehicle> Wake()
		{
			var response = await getTeslaAPI<ResponseWrapper<Vehicle>>($"api/1/vehicles/{VehicleId}/wake_up", HttpMethod.Post);
			return response?.Item;
		}

		/// <summary>
		/// Lock or unlock the doors
		/// </summary>
		/// <param name="lockMode">True to lock the doors, False to unlock the doors</param>
		/// <returns><see cref="CommandResponse"/></returns>
		public async Task<CommandResponse> LockDoors(bool lockMode = true)
		{
			var endpoint = lockMode ? "door_lock" : "door_unlock";
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/{endpoint}", HttpMethod.Post);
			return response?.Item;
		}

		/// <summary>
		/// Open or close the trunk, if possible
		/// </summary>
		/// <param name="rearTrunk">True to operate rear trunk, False to operate front trunk</param>
		/// <returns><see cref="CommandResponse"/></returns>
		public async Task<CommandResponse> ToggleTrunk(bool rearTrunk = true)
		{
			var whichTrunk = rearTrunk ? "rear" : "front";
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/actuate_trunk?which_trunk={whichTrunk}", HttpMethod.Post);
			return response?.Item;
		}

		/// <summary>
		/// Honk the horn
		/// </summary>
		/// <returns><see cref="CommandResponse"/></returns>
		public async Task<CommandResponse> Honk()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/honk_horn", HttpMethod.Post);
			return response?.Item;
		}

		/// <summary>
		/// Flash the headlights
		/// </summary>
		/// <returns><see cref="CommandResponse"/></returns>
		public async Task<CommandResponse> FlashLights()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/flash_lights", HttpMethod.Post);
			return response?.Item;
		}

		/// <summary>
		/// Turn on or off the HVAC system
		/// </summary>
		/// <param name="turnOnHvac">True to turn on HVAC, False to turn off HVAC</param>
		/// <returns></returns>
		public async Task<CommandResponse> Hvac(bool turnOnHvac = true)
		{
			var endpoint = turnOnHvac ? "auto_conditioning_start" : "auto_conditioning_stop";
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/{endpoint}", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> SetTemperatureF(int tempDriverF, int tempPassengerF)
		{
			var url = string.Format("api/1/vehicles/{0}/command/set_temps?driver_temp={1:3.1}&passenger_temp={2:3.1}",
				VehicleId,
				tempDriverF,
				tempPassengerF);

			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>(url, HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> SetChargeLimit(int chargeLimitPercent)
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/set_charge_limit?percent={chargeLimitPercent}", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> SetMaxRangeChargeLimit()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/charge_max_range", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> SetStandardRangeChargeLimit()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/charge_standard", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> RemoteStart(string password)
		{
			var formdata = new Dictionary<string, string>();
			formdata["password"] = password;

			return await getTeslaAPI<CommandResponse>($"api/1/vehicles/{VehicleId}/command/remote_start_drive", HttpMethod.Post, formdata: formdata);
		}

		/// <summary>
		/// Open or close the charging port door
		/// </summary>
		/// <param name="openChargePort">True to open the port, False to close the port</param>
		/// <returns><see cref="CommandResponse"/></returns>
		public async Task<CommandResponse> ChargePort(bool openChargePort = true)
		{
			var endpoint = openChargePort ? "charge_port_door_open" : "charge_port_door_close";
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/{endpoint}", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> StartCharging()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/charge_start", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> StopCharging()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/charge_stop", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> Homelink(float latitude, float longitude)
		{
			var formdata = new Dictionary<string, string>();
			formdata["lat"] = latitude.ToString();
			formdata["long"] = latitude.ToString();

			return await getTeslaAPI<CommandResponse>($"api/1/vehicles/{VehicleId}/command/trigger_homelink", HttpMethod.Post, formdata: formdata);
		}

		public async Task<CommandResponse> SpeedLimit(int limitMph)
		{
			var formdata = new Dictionary<string, string>();
			formdata["limit_mph"] = limitMph.ToString();

			return await getTeslaAPI<CommandResponse>($"api/1/vehicles/{VehicleId}/command/speed_limit_set_limit", HttpMethod.Post, formdata: formdata);
		}

		/// <summary>
		/// Turn Sentry Mode on or off
		/// </summary>
		/// <param name="sentryModeOn">True to turn sentry mode on, False to turn sentry mode off</param>
		/// <returns><see cref="CommandResponse"/></returns>
		public async Task<CommandResponse> SentryMode(bool sentryModeOn)
		{
			var formdata = new Dictionary<string, string>();
			formdata["on"] = sentryModeOn ? "true" : "false";

			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/set_sentry_mode", HttpMethod.Post, formdata: formdata);
			return response?.Item;
		}

		/// <summary>
		/// Operate the media control system (audio/video)
		/// </summary>
		/// <param name="command"><see cref="MediaCommand"/></param>
		/// <returns><see cref="CommandResponse"/></returns>
		public async Task<CommandResponse> MediaControl (MediaCommand command)
		{
			string endpoint = null;
			switch (command)
			{
				case MediaCommand.TogglePlayback:
					endpoint = "media_toggle_playback";
					break;

				case MediaCommand.NextTrack:
					endpoint = "media_next_track";
					break;

				case MediaCommand.PreviousTrack:
					endpoint = "media_next_track";
					break;

				case MediaCommand.NextFavorite:
					endpoint = "media_next_fav";
					break;

				case MediaCommand.PreviousFavorite:
					endpoint = "media_prev_fav";
					break;

				case MediaCommand.VolumeUp:
					endpoint = "media_volume_up";
					break;

				case MediaCommand.VolumeDown:
					endpoint = "media_volume_down";
					break;
			}

			if (endpoint == null)
				return null;

			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/{endpoint}", HttpMethod.Post);
			return response?.Item;
		}
	}
}
