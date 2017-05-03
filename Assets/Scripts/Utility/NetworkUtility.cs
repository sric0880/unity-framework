using UnityEngine;

public static class NetworkUtility
{
	/// <summary>
	/// 网络可用
	/// </summary>
	public static bool NetAvailable {
		get {
			return Application.internetReachability != NetworkReachability.NotReachable;
		}
	}

	/// <summary>
	/// 是否是无线
	/// </summary>
	public static bool IsWifi {
		get {
			return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
		}
	}
}

