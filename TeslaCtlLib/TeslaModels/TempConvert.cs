﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TeslaLib.TeslaModels
{
	public static class TempConvert
	{
		public static float ToFahrenheit(float celsius)
		{
			return ((9.0f / 5.0f) * celsius) + 32.0f;
		}
	}
}
