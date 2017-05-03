#if UNITY_IOS
using UnityEngine.iOS;
using System;

public static class LocalNotificationsHelper
{
	public static void CancelAllLocalNotifications()
	{
		NotificationServices.CancelAllLocalNotifications();
	}

	public static void ScheduleLocalNotification(int id, DateTime time, string titleOnAndroid, string actionName,
		string message, bool repeatEveryday = false)
	{
		var notif = new LocalNotification
		{
			alertAction = actionName,
			alertBody = message,
			applicationIconBadgeNumber = 1,
			fireDate = time.ToUniversalTime(),
			hasAction = true
		};
		if (repeatEveryday) notif.repeatInterval = CalendarUnit.Day;
		NotificationServices.ScheduleLocalNotification(notif);
	}
}
#endif