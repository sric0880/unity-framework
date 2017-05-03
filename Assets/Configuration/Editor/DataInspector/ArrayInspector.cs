using UnityEditor;
using UnityEngine;
using System;

public class ArrayInspector : DataInspector {

	public GUIStyle btnStype = new GUIStyle(GUI.skin.button);

	public override bool canFoldout()
	{
		return true;
	}

	public override bool printHead(ref object data, Type type) 
	{
		btnStype.alignment = TextAnchor.MiddleRight;
		EditorGUILayout.LabelField("[Array of "+ type.GetElementType().Name + "]");
		if (GUILayout.Button("+", btnStype, GUILayout.Width(20f)))
		{
			if (data == null) 
			{
				data = InstanceUtility.InstanceOfType(type);
			}
			else
			{
				var array = data as Array;
				var newArray = InstanceUtility.resizeArray(array, array.Length + 1);
				data = newArray;
			}
			return true;
		}
		if (GUILayout.Button("-", btnStype, GUILayout.Width(20)))
		{
			if (data == null) return false;
			var array = data as Array;
			if (array.Length == 0) return false;
			var newArray = InstanceUtility.resizeArray(array, array.Length - 1);
			data = newArray;
			return true;
		}
		return false;
	}
	
	public override bool inspect(ref object data, Type type, string name, string path)
	{
		if (data == null) return false;
		var array = data as Array;

		var valueType = type.GetElementType();
		bool changed = false;
		for (int i = 0; i < array.Length; ++i)
		{
			object value = array.GetValue(i);
			if (DataInspectorUtility.inspect(ref value, valueType, "["+i+"]", path))
			{
				changed = true;
				array.SetValue(value, i);
			}
		}
		return changed;
	}
}
