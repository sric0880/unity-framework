using UnityEngine;

public class GUIConsolePageSystemInfo : GUIConsolePage
{
	private Vector2 scrollPos = Vector2.zero;

	public GUIConsolePageSystemInfo(string name)
	{
		this.PageName = name;
	}

	public override void Start()
	{
	}

	public override void Exit()
	{
	}

	public override void OnGUI()
	{
		scrollPos = GUILayout.BeginScrollView(scrollPos);
		GUILayout.Label("device model: " + SystemInfo.deviceModel);
		GUILayout.Label("device name: " + SystemInfo.deviceName);
		GUILayout.Label("device type: " + SystemInfo.deviceType);
		GUILayout.Label("device unique id: " + SystemInfo.deviceUniqueIdentifier);
		GUILayout.Label("device GPU type: " + SystemInfo.graphicsDeviceType);
		GUILayout.Label("device GPU vendor: " + SystemInfo.graphicsDeviceVendor);
		GUILayout.Label("device GPU memory: " + SystemInfo.graphicsMemorySize);
		GUILayout.Label("using multi-threaded rendering: " + SystemInfo.graphicsMultiThreaded);
		GUILayout.Label("GPU shader level: " + SystemInfo.graphicsShaderLevel);
		GUILayout.Label("npot supported: " + SystemInfo.npotSupport);
		GUILayout.Label("os: " + SystemInfo.operatingSystem);
		GUILayout.Label("accelerometer supported: " + SystemInfo.supportsAccelerometer);
		GUILayout.Label("image effects supported: " + SystemInfo.supportsImageEffects);
		GUILayout.Label("compute shaders supported: " + SystemInfo.supportsComputeShaders);
		GUILayout.Label("system memory size: " + SystemInfo.systemMemorySize);
		GUILayout.EndScrollView();
	}
}