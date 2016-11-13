using System;
using System.Reflection;
using System.Collections.Generic;

public static class ClassFieldFilter {

	public static List<FieldInfo> GetClassFieldInfo (Type type)
	{
		//get public static and notstatic members
		List<FieldInfo> fields = new List<FieldInfo> ();
		fields.AddRange (type.GetFields (BindingFlags.Instance | BindingFlags.Public));
		fields.AddRange (type.GetFields (BindingFlags.Static | BindingFlags.Public));
		for (int i = 0; i < fields.Count;)
		{
			if (fields[i].IsLiteral || fields[i].IsInitOnly) //const members or readonly
				fields.RemoveAt(i);
			else ++i;
		}
		//sort fields
		fields.Sort ((a, b) =>  {
			return string.Compare (a.Name, b.Name);
		});
		return fields;
	}

	public static List<FieldInfo> GetConfigFieldInfo(Type type)
	{
		var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
		List<FieldInfo> exportFields = new List<FieldInfo>();
		foreach(var field in fields)
		{
			if(field.IsDefined(typeof(ExportAttribute), true))
			{
				exportFields.Add(field);
			}
		}
		//sort fields
		exportFields.Sort ((a, b) =>  {
			return string.Compare (a.Name, b.Name);
		});
		return exportFields;
	}
}
