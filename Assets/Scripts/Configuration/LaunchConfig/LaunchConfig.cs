using System;

public static class LaunchConfig
{
	/// <summary>
	/// Need initialization
	/// </summary>
	public static ConfContext context = new ConfContext();

	[Export] public static ConfBoot boot = new ConfBoot();
	[Export] public static ConfUpdate update = new ConfUpdate();
}