using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class ListInspector : DataInspector {

	public GUIStyle btnStype = new GUIStyle(GUI.skin.button);

	public override bool canFoldout()
	{
		return true;
	}

	public override bool printHead(ref object data, Type type) 
	{
		btnStype.alignment = TextAnchor.MiddleRight;
		EditorGUILayout.LabelField("[List<"+ type.GetGenericArguments()[0].Name + ">]");
		if (GUILayout.Button("+", btnStype, GUILayout.Width(20f)))
		{
			if (data == null)
			{
				data = InstanceUtility.InstanceOfType(type);
			}
			else
			{
				var list = data as IList;
				InstanceUtility.resizeList(list, list.Count + 1);
			}
			return true;
		}
		if (GUILayout.Button("-", btnStype, GUILayout.Width(20)))
		{
			if (data == null) return false;
			var list = data as IList;
			if (list.Count == 0) return false;
			InstanceUtility.resizeList(list, list.Count - 1);
			return true;
		}
		return false;
	}
	
	public override bool inspect(ref object data, Type type, string name, string path)
	{
		var list = data as IList;
		if (list == null) return false;

		var valueType = type.GetGenericArguments()[0];
		bool changed = false;
		for (int i = 0; i < list.Count; ++i)
		{
			var value = list[i];
			if (DataInspectorUtility.inspect(ref value, valueType, "["+i+"]", path))
			{
				changed = true;
				list[i] = value;
			}
		}

		return changed;
	}
}
