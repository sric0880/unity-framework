using UnityEngine;
using UnityEditor;
using System;

public class DecimalInspector : DataInspector{

	public override bool canFoldout()
	{
		return false;
	}
	
	public override bool inspect(ref object data, Type type, string name, string path)
	{
		decimal temp;
		if (decimal.TryParse(EditorGUILayout.TextField(name, data.ToString()), out temp))
		{
			return applyData(ref data, temp);
		}
		return false;
	}
}
