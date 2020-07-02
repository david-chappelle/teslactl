using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TeslaLib
{
	public partial class TeslaClient
	{
		public Uri BaseUri { get; set; }
		public string AccessToken { get; set; }
		public HttpClient HttpClient { get; set; }

		public TeslaClient()
		{
			BaseUri = new Uri("https://owner-api.teslamotors.com");
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
				return default(T);
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

		private JsonSerializerSettings _serializerSettings;
	}
}
