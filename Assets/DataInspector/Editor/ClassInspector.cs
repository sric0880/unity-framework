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
		bool changed = false;
		var fields = TypeUtility.GetClassFieldInfo(type);
		var properties = TypeUtility.GetClassPropertyInfo(type);
		if (fields.Count == 0 && properties.Count == 0)
		{
			return changed;
		}
		//inspect the fields
		SortedDictionary<string, FieldInfo> fieldDict = new SortedDictionary<string, FieldInfo>();
		foreach (var fieldinfo in fields)
		{
			string fieldname = fieldinfo.Name;
			fieldDict.Add(fieldname, fieldinfo);
		}
		foreach (var keyvalue in fieldDict)
		{
			object value = keyvalue.Value.GetValue(data);
			if (value == null) continue;
			Type valueType = value.GetType();
			if (DataInspectorUtility.inspect(ref value, valueType, keyvalue.Key, path))
			{
				keyvalue.Value.SetValue(data, value);
				changed = true;
			}
		}
		foreach (var propertyInfo in properties)
		{
			object value = propertyInfo.GetValue(data, null);
			if (value == null) continue;
			Type valueType = value.GetType();
			if (DataInspectorUtility.inspect(ref value, valueType, propertyInfo.Name, path))
			{
				propertyInfo.SetValue(data, value, null);
				changed = true;
			}
		}
		return changed;
	}
}
