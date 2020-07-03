using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TeslaCtlLib
{
	public class TeslaApiException : Exception
	{
		public string Endpoint { get; internal set; }
		public HttpResponseMessage Response { get; internal set; }

		public TeslaApiException(HttpResponseMessage response, string endpoint)
		{
			Endpoint = endpoint;
			Response = response;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine($"TeslaApiException: {Endpoint ?? string.Empty}");

			if (Response != null)
				sb.AppendLine($"  Code: {(int) Response.StatusCode} {Response.StatusCode} \"{Response.ReasonPhrase}\"");

			return sb.ToString();
		}
	}
}
