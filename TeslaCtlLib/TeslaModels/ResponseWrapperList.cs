using Newtonsoft.Json;

namespace TeslaLib.TeslaModels
{
	public class ResponseWrapperList<T>
	{
		[JsonProperty("response")]
		public T[] Items { get; set; }

		public int Count { get; set; }
	}
}
