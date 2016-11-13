using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;

public class ClassInspector : DataInspector {

	public override bool canFoldout()
	{
		return true;
	}
	
	public override bool inspect(ref object data, Type type, string name, string path)
	{
		var fields = ClassFieldFilter.GetClassFieldInfo(type);
		//inspect the fields
		bool changed = false;
		foreach (var fieldinfo in fields)
		{
			object value = fieldinfo.GetValue(data);
			Type valueType = value!=null ? value.GetType() : fieldinfo.FieldType;
			if (DataInspectorUtility.inspect(ref value, valueType, fieldinfo.Name, path))
			{
				fieldinfo.SetValue(data, value);
				changed = true;
			}
		}
		return changed;
	}

}
