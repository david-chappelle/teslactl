using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TeslaLib.TeslaModels;

namespace TeslaLib
{
	public partial class TeslaClient
	{
		public async Task<AuthenticationResponse> Authenticate(string clientId, string clientSecret, string email, string password)
		{
			var requestData = new Dictionary<string, string>();
			requestData["grant_type"] = "password";
			requestData["client_id"] = clientId;
			requestData["client_secret"] = clientSecret;
			requestData["email"] = email;
			requestData["password"] = password;

			return await getTeslaAPI<AuthenticationResponse>("/oauth/token", method: HttpMethod.Post, formdata: requestData);
		}

		public async Task<AuthenticationResponse> RefreshToken(string clientId, string clientSecret, string refreshToken)
		{
			var requestData = new Dictionary<string, string>();
			requestData["grant_type"] = "refresh_token";
			requestData["client_id"] = clientId;
			requestData["client_secret"] = clientSecret;
			requestData["refresh_token"] = refreshToken;

			return await getTeslaAPI<AuthenticationResponse>("/oauth/token", method: HttpMethod.Post, formdata: requestData);
		}
	}
}
