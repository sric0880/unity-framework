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
		SortedDictionary<string, FieldInfo> fieldDict = new SortedDictionary<string, FieldInfo>();
		foreach (var fieldinfo in fields)
		{
			var exportAttr = TypeUtility.GetCustomAttribute<ExportAttribute>(fieldinfo);
			string fieldname= exportAttr == null || string.IsNullOrEmpty(exportAttr.Name) ? fieldinfo.Name : exportAttr.Name;
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
		return changed;
	}
}
