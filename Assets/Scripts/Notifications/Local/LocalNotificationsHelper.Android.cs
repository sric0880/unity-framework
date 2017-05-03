#if UNITY_ANDROID
using System;

public static class LocalNotificationsHelper
{
	public static void CancelAllLocalNotifications()
	{
//		try
//		{
//			if (!PlatformInfo.IsEditor)
//			{
//				AndroidJavaClass clazz = new AndroidJavaClass("com.pwrd.orion.abtest.Alarm");
//				clazz.CallStatic("ClearAlarmNotification");
//			}
//		}
//		catch (Exception ex)
//		{
//			LogMan.Warning("Cannot clear alarm notification " + ex);
//		}
	}

	public static void ScheduleLocalNotification(int id, DateTime time, string titleOnAndroid, string actionName,
		string message, bool repeatEveryday = false)
	{
//	try
//		{
//			if (!PlatformInfo.IsEditor)
//			{
//				TimeSpan span = time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
//				AndroidJavaClass clazz = new AndroidJavaClass("com.pwrd.orion.abtest.Alarm");
//				clazz.CallStatic("SetAlarmNotification", id, (long)span.TotalMilliseconds, 
//					(long)(repeatEveryday ? TimeSpan.FromDays(1).TotalMilliseconds : 0), titleOnAndroid, message);
//			}
//		}
//		catch (Exception ex)
//		{
//			LogMan.Warning("Cannot schedule alarm notification " + ex);
//		}
	}
}
#endif