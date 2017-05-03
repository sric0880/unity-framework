using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour {

	private void Awake()
	{
		Application.targetFrameRate = 30;

		GameFSM.Instance.Start();

		//TODO: 3rd party do it
		LocalNotificationsHelper.CancelAllLocalNotifications();

		InitManagers();
	}

	private void Start () {
		
	}
	
	private void Update () {
		GameFSM.Instance.Update();
	}

	private void LateUpdate()
	{
		
	}

	private void OnDestroy()
	{
		Log.Info("App destroy");
	}

	private void OnApplicationPause()
	{
		Log.Info("Application pause");
	}

	private void OnApplicationFocus()
	{
		Log.Info("Application focus");
	}

	private void OnApplicationQuit()
	{
		//TODO fire local notifications
		Log.Info("Application quit");
	}

	private void OnDrawGizmos()
	{
		
	}

	private void InitManagers()
	{
		LuaFramework.LuaManager.Instance.OnInit();
	}
}
