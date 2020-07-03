using System.Threading.Tasks;
using TeslaLib.TeslaModels;

namespace TeslaLib
{
	public partial class TeslaClient
	{
		public async Task<VehicleData> QueryVehicleData()
		{
			var response = await getTeslaAPI<ResponseWrapper<VehicleData>>($"api/1/vehicles/{VehicleId}/vehicle_data");
			return response?.Item;
		}

		public async Task<ServiceData> QueryVehicleServiceData()
		{
			var response = await getTeslaAPI<ResponseWrapper<ServiceData>>($"api/1/vehicles/{VehicleId}/service_data");
			return response?.Item;
		}

		public async Task<Vehicle[]> QueryVehicles()
		{
			var response = await getTeslaAPI<ResponseWrapperList<Vehicle>>("api/1/vehicles");
			return response?.Items;
		}

		public async Task<ChargeState> QueryChargeState()
		{
			var response = await getTeslaAPI<ResponseWrapper<ChargeState>>($"api/1/vehicles/{VehicleId}/data_request/charge_state");
			return response?.Item;
		}

		public async Task<ClimateState> QueryClimateState()
		{
			var response = await getTeslaAPI<ResponseWrapper<ClimateState>>($"api/1/vehicles/{VehicleId}/data_request/climate_state");
			return response?.Item;
		}

		public async Task<DriveState> QueryDriveState()
		{
			var response = await getTeslaAPI<ResponseWrapper<DriveState>>($"api/1/vehicles/{VehicleId}/data_request/drive_state");
			return response?.Item;
		}

		public async Task<GuiSettings> QueryGuiSettings()
		{
			var response = await getTeslaAPI<ResponseWrapper<GuiSettings>>($"api/1/vehicles/{VehicleId}/data_request/gui_settings");
			return response?.Item;
		}

		public async Task<VehicleState> QueryVehicleState()
		{
			var response = await getTeslaAPI<ResponseWrapper<VehicleState>>($"api/1/vehicles/{VehicleId}/data_request/vehicle_state");
			return response?.Item;
		}

		public async Task<bool> QueryMobileEnabled()
		{
			var response = await getTeslaAPI<ResponseWrapper<bool>>($"api/1/vehicles/{VehicleId}/mobile_enabled");
			return response.Item;
		}

		public async Task<NearbyChargingSites> QueryNearbyChargingSites()
		{
			var response = await getTeslaAPI<ResponseWrapper<NearbyChargingSites>>($"api/1/vehicles/{VehicleId}/nearby_charging_sites");
			return response?.Item;
		}

		public async Task<MessageList> QueryMessages()
		{
			var response = await getTeslaAPI<ResponseWrapper<MessageList>>($"api/1/messages");
			return response?.Item;
		}

		public async Task<object> QueryUpgradeEligibility()
		{
			var response = await getTeslaAPI<ResponseWrapper<object>>($"api/1/vehicles/{VehicleId}/eligibility");
			return response?.Item;
		}
	}
}