using System;
using System.Collections.Generic;
using System.Text;

namespace TeslaLib.TeslaModels
{
	public class SpeedLimitMode
	{
		public bool Active { get; set; }
		public float CurrentLimitMph { get; set; }
		public int MaxLimitMph { get; set; }
		public int MinLimitMph { get; set; }
		public bool PinCodeSet { get; set; }
	}
}
