
using Newtonsoft.Json;

namespace TeslaLib.TeslaModels
{
	public class ResponseWrapper<T>
	{
		[JsonProperty("response")]
		public T Item { get; set; }
	}
}
