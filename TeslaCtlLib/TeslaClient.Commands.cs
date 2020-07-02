using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TeslaLib.TeslaModels;

namespace TeslaLib
{
	public partial class TeslaClient
	{
		public async Task<Vehicle> Wake(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<Vehicle>>($"api/1/vehicles/{id}/wake_up", HttpMethod.Post);
			return response.Item;
		}

		public async Task<CommandResponse> UnlockDoors(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<CommandResponse>>($"api/1/vehicles/{id}/command/door_unlock", HttpMethod.Post);
			return response?.Item;
		}

		public async Task<CommandResponse> Honk(long id)
		{
			return await getTeslaAPI<CommandResponse>($"api/1/vehicles/{id}/command/honk_horn", HttpMethod.Post);
		}

		public async Task<CommandResponse> FlashLights(long id)
		{
			return await getTeslaAPI<CommandResponse>($"api/1/vehicles/{id}/command/flash_lights", HttpMethod.Post);
		}

		public async Task<CommandResponse> RemoteStart(long id, string password)
		{
			var formdata = new Dictionary<string, string>();
			formdata["password"] = password;

			return await getTeslaAPI<CommandResponse>($"api/1/vehicles/{id}/command/remote_start_drive", HttpMethod.Post, formdata: formdata);
		}

		public async Task<CommandResponse> Homelink(long id, float latitude, float longitude)
		{
			var formdata = new Dictionary<string, string>();
			formdata["lat"] = latitude.ToString();
			formdata["long"] = latitude.ToString();

			return await getTeslaAPI<CommandResponse>($"api/1/vehicles/{id}/command/trigger_homelink", HttpMethod.Post, formdata: formdata);
		}

		public async Task<CommandResponse> SpeedLimit(long id, int limitMph)
		{
			var formdata = new Dictionary<string, string>();
			formdata["limit_mph"] = limitMph.ToString();

			return await getTeslaAPI<CommandResponse>($"api/1/vehicles/{id}/command/speed_limit_set_limit", HttpMethod.Post, formdata: formdata);
		}
	}
}
