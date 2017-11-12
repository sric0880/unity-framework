using UniRx;
using System;
using UnityEngine;

public static class SchedulerUtils
{
	public static IDisposable MainThread_Invoke(Action action)
	{
		return Scheduler.MainThread.Schedule(action);
	}

	// delaySeconds zero means next frame
	public static IDisposable MainThread_DelayInvoke(Action action, float delaySeconds)
	{
		return Scheduler.MainThread.Schedule(TimeSpan.FromSeconds(delaySeconds), action);
	}

	public static IDisposable MainThread_DelayInvoke(GameObject gameObject, Action action, float delaySeconds)
	{
		return Scheduler.MainThread.Schedule(TimeSpan.FromSeconds(delaySeconds), action).AddTo(gameObject);
	}

	// durSeconds zero means every frame
	public static IDisposable MainThread_LoopInvoke(Action action, float durSeconds)
	{
		return (Scheduler.MainThread as ISchedulerPeriodic).SchedulePeriodic(TimeSpan.FromSeconds(durSeconds), action);
	}

	public static IDisposable MainThread_LoopInvoke(GameObject gameObject, Action action, float durSeconds)
	{
		return (Scheduler.MainThread as ISchedulerPeriodic).SchedulePeriodic(TimeSpan.FromSeconds(durSeconds), action).AddTo(gameObject);
	}

	public static void Cancel(IDisposable  d)
	{
		d.Dispose();
	}
}
