using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class TimeUtility {
	public static long GetTime()
	{
		TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
		return (long)ts.TotalMilliseconds;
	}
}
