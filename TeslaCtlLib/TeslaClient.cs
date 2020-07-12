using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TeslaCtlLib;
using System.Threading;

namespace TeslaLib
{
	public partial class TeslaClient
	{
		public Uri BaseUri { get; set; }
		public string AccessToken { get; set; }
		public HttpClient HttpClient { get; set; }
		public long VehicleId { get; set; }

		public TeslaClient()
		{
			HttpClient = new HttpClient();
			HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd("TeslaCtl/1.0");

			_serializerSettings = new JsonSerializerSettings()
			{
				ContractResolver = new DefaultContractResolver()
				{
					NamingStrategy = new SnakeCaseNamingStrategy()
				}
			};
		}

		private async Task<T> getTeslaAPI<T>(string endpoint, HttpMethod method = null, string requestPayload = null, IDictionary<string, string> formdata = null)
		{
			var requestUri = new Uri(BaseUri, endpoint);
			var requestMessage = getRequestMessageWithAuth(requestUri, method ?? HttpMethod.Get, requestPayload, formdata);
			var responseMessage = await HttpClient.SendAsync(requestMessage);
			if (responseMessage.IsSuccessStatusCode)
			{
				var response = await responseMessage.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(response, _serializerSettings);
			}
			else
				throw new TeslaApiException(responseMessage, endpoint);
		}

		private HttpRequestMessage getRequestMessageWithAuth(Uri requestUri, HttpMethod method, string requestPayload, IDictionary<string,string> formdata)
		{
			var requestMessage = new HttpRequestMessage(method, requestUri);

			if (!string.IsNullOrEmpty(AccessToken))
				requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

			if (requestPayload != null)
				requestMessage.Content = new StringContent(requestPayload);
			else if (formdata != null)
				requestMessage.Content = new FormUrlEncodedContent(formdata);

			return requestMessage;
		}

		public async Task<bool> ForceWake(int timeout = Timeout.Infinite)
		{
			var start = DateTime.Now;
			var vehicleIsAwake = false;
			var stopTrying = false;

			do
			{
				var response = await Wake();

				if (response.State == "online")
					vehicleIsAwake = true;
				else if (timeout != Timeout.Infinite && (DateTime.Now - start).TotalSeconds > timeout)
					stopTrying = true;
				else
					await Task.Delay(5000);
			}
			while (!vehicleIsAwake && !stopTrying);

			return vehicleIsAwake;
		}

		private JsonSerializerSettings _serializerSettings;
	}
}
