using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TeslaLib.TeslaModels;

namespace TeslaLib
{
	// Tesla commands
	// https://www.teslaapi.io/vehicles/commands

	public partial class TeslaClient
	{
		public async Task<Vehicle> Wake()
		{
			var response = await getTeslaAPI<ResponseWrapper<Vehicle>>($"api/1/vehicles/{VehicleId}/wake_up", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> UnlockDoors()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/door_unlock", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> LockDoors()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/door_lock", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> Honk()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/honk_horn", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> FlashLights()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/flash_lights", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> StartHvac()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/auto_conditioning_start", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> StopHvac()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/auto_conditioning_stop", HttpMethod.Post);
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

		public async Task<CommandResponse> OpenChargePort()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/charge_port_door_open", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> CloseChargePort()
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/charge_port_door_close", HttpMethod.Post);
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

		public async Task<CommandResponse> SentryMode(bool sentryModeOn)
		{
			var formdata = new Dictionary<string, string>();
			formdata["on"] = sentryModeOn ? "true" : "false";

			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{VehicleId}/command/set_sentry_mode", HttpMethod.Post, formdata: formdata);
			return response?.Item;
		}
	}
}
