using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class ConfigInspector : EditorWindow{

	[MenuItem("Window/Inspector/Config")]
	private static void OpenConfigEditor()
	{
		ConfigInspector window = (ConfigInspector)EditorWindow.GetWindow(typeof(ConfigInspector));
		window.Show();
	}

	private Vector2 scrollPos;

	private void OnGUI()
	{
		//static config inspector
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		object obj = UF.Managers.ConfigManager.Instance;
		DataInspectorUtility.inspect(ref obj, typeof(UF.Managers.ConfigManager), "Config");
		EditorGUILayout.EndScrollView();
	}
}
