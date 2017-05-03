using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class ConfigInspector : EditorWindow{

	[MenuItem("Window/Inspector/Configuration")]
	private static void OpenConfigEditor()
	{
		ConfigInspector window = (ConfigInspector)EditorWindow.GetWindow(typeof(ConfigInspector));
		window.Show();
	}

	private Vector2 scrollPos;

	private List<int> changedIndex = new List<int>();
	private bool changed
	{
		get { return changedIndex.Count > 0; }
	}

	private void OnGUI()
	{
		//static config inspector
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		for (int i = 0; i < CodegenExe.staticTypes.Length; ++i)
		{
			if (printStatic(CodegenExe.staticTypes[i], CodegenExe.typeNames[i]))
			{
				changedIndex.Add(i);
			}
		}

		EditorGUILayout.EndScrollView();
	}

	private bool printStatic(Type type, string showname)
	{
		var exportFields = ClassFieldFilter.GetConfigFieldInfo(type);

		if (exportFields.Count == 0)
		{
			return false;
		}

		object NullObj = null;
		return DataInspectorUtility.inspect(ref NullObj, type, showname);
	}
}
