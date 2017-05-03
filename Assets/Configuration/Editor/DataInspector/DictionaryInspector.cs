using UnityEditor;
using UnityEngine;
using System;
using System.Collections;

public class DictionaryInspector :  DataInspector{

	public GUIStyle btnStype = new GUIStyle(GUI.skin.button);

	public override bool canFoldout()
	{
		return true;
	}

	public override bool printHead(ref object data, Type type) 
	{
		btnStype.alignment = TextAnchor.MiddleRight;
		var args = type.GetGenericArguments();
		var keyType = args[0];
		var valueType = args[1];
		EditorGUILayout.LabelField("[Dict<"+keyType.Name +", "+ valueType.Name  + ">]");
		if (GUILayout.Button("+", btnStype, GUILayout.Width(20f)))
		{
			if (data == null)
				data = InstanceUtility.InstanceOfType(type);
			else
				InstanceUtility.addKeyValuePair(data as IDictionary, keyType, valueType);
			return true;
		}
		return false;
	}
	
	public override bool inspect(ref object data, Type type, string name, string path)
	{
		if (data == null) return false;
		var dict = data as IDictionary;

		var args = type.GetGenericArguments();
		var keyType = args[0];
		var valueType = args[1];

		bool changed = false;
		object keyToDelete = null;
		DictionaryEntry pairToAdd = new DictionaryEntry();
		DictionaryEntry pairToChange = new DictionaryEntry();
		foreach(DictionaryEntry pair in dict)
		{
			var key = pair.Key;
			var value = pair.Value;

			EditorGUILayout.BeginHorizontal();
			if (DataInspectorUtility.inspect(ref key, keyType, "Key", path + key.ToString()))
			{
				if (dict.Contains(key))
				{
					continue;
				}
				keyToDelete = pair.Key;
				pairToAdd = new DictionaryEntry(key, value);
			}

			if ( GUILayout.Button("-", btnStype, GUILayout.Width(20f)))
			{
				keyToDelete = key;
				continue;
			}

			EditorGUILayout.EndHorizontal();
			if (DataInspectorUtility.inspect(ref value, valueType, "Value", path + key.ToString()))
			{
				pairToChange = new DictionaryEntry(key, value);
			}
		}
		if (keyToDelete != null)
		{
			DataInspectorUtility.removeChangedPath(path+keyToDelete.ToString()+".Key");
			dict.Remove(keyToDelete);
			changed = true;
		}
		if (pairToAdd.Key != null && pairToAdd.Value != null)
		{
			DataInspectorUtility.addChangedPath(path+pairToAdd.Key.ToString()+".Key");
			dict.Add(pairToAdd.Key, pairToAdd.Value);
			changed = true;
		}
		if (pairToChange.Key != null && pairToChange.Value != null)
		{
			dict[pairToChange.Key] = pairToChange.Value;
			changed = true;
		}
		return changed;
	}
}
