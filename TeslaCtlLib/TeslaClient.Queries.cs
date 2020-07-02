using System.Threading.Tasks;
using TeslaLib.TeslaModels;

namespace TeslaLib
{
	public partial class TeslaClient
	{
		public async Task<Vehicle[]> QueryVehicles()
		{
			var response = await getTeslaAPI<ResponseWrapperList<Vehicle>>("api/1/vehicles");
			return response.Items;
		}

		public async Task<VehicleData> QueryVehicleData(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<VehicleData>>($"api/1/vehicles/{id}/vehicle_data");
			return response.Item;
		}

		public async Task<ServiceData> QueryVehicleServiceData(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<ServiceData>>($"api/1/vehicles/{id}/service_data");
			return response.Item;
		}

		public async Task<ChargeState> QueryChargeState(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<ChargeState>>($"api/1/vehicles/{id}/data_request/charge_state");
			return response.Item;
		}

		public async Task<ClimateState> QueryClimateState(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<ClimateState>>($"api/1/vehicles/{id}/data_request/climate_state");
			return response.Item;
		}

		public async Task<DriveState> QueryDriveState(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<DriveState>>($"api/1/vehicles/{id}/data_request/drive_state");
			return response.Item;
		}

		public async Task<GuiSettings> QueryGuiSettings(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<GuiSettings>>($"api/1/vehicles/{id}/data_request/gui_settings");
			return response.Item;
		}

		public async Task<VehicleState> QueryVehicleState(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<VehicleState>>($"api/1/vehicles/{id}/data_request/vehicle_state");
			return response.Item;
		}

		public async Task<bool> QueryMobileEnabled(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<bool>>($"api/1/vehicles/{id}/mobile_enabled");
			return response.Item;
		}

		public async Task<NearbyChargingSites> QueryNearbyChargingSites(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<NearbyChargingSites>>($"api/1/vehicles/{id}/nearby_charging_sites");
			return response.Item;
		}

		public async Task<MessageList> QueryMessages()
		{
			var response = await getTeslaAPI<ResponseWrapper<MessageList>>($"api/1/messages");
			return response.Item;
		}

		public async Task<object> QueryUpgradeEligibility(long id)
		{
			var response = await getTeslaAPI<ResponseWrapper<object>>($"api/1/vehicles/{id}/eligibility");
			return response?.Item;
		}
	}
}