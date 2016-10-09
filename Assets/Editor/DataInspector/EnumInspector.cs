using UnityEditor;
using System;

public class EnumInspector : DataInspector {

	public override bool canFoldout()
	{
		return false;
	}
	
	public override bool inspect(ref object data, Type type, string name, string path)
	{
		Enum e = data as Enum;
		if (e == null)
			return false;
		return applyData(ref data, EditorGUILayout.EnumPopup(name, e));
	}

}
