using System;
using System.Collections.Generic;
using System.Text;

namespace TeslaLib.TeslaModels
{
	public class SoftwareUpdate
	{
		public int DownloadPerc { get; set; }
		public int ExpectedDurationSec { get; set; }
		public int InstallPerc { get; set; }
		public string Status { get; set; }
		public string Version { get; set; }
	}
}
