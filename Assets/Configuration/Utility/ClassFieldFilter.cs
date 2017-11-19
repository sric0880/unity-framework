using System;
using System.Reflection;
using System.Collections.Generic;

[Flags]
public enum ExportAttributeType
{
	CS = 1,
	LUA = 2,
}

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
		// validate field attribute
		foreach (var field in fields)
		{
			TypeUtility.ValidateAttributeType(field, type.Name);
		}
		//sort fields
		fields.Sort ((a, b) =>  {
			return string.Compare (a.Name, b.Name);
		});
		return fields;
	}

	public static List<PropertyInfo> GetClassPropertyInfo(Type type)
	{
		//get public static and notstatic members
		List<PropertyInfo> properties = new List<PropertyInfo>();
		properties.AddRange(type.GetProperties(BindingFlags.Instance | BindingFlags.Public));
		properties.AddRange(type.GetProperties(BindingFlags.Static | BindingFlags.Public));
		//sort fields
		properties.Sort((a, b) =>
		{
			return string.Compare(a.Name, b.Name);
		});
		return properties;
	}

	public static List<FieldInfo> GetConfigFieldInfo(Type type, ExportAttributeType attrType = ExportAttributeType.CS)
	{
		var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
		List<FieldInfo> exportFields = new List<FieldInfo>();
		foreach(var field in fields)
		{
			if ((ExportAttributeType.CS & attrType) > 0)
			{ 
				if (field.IsDefined(typeof(ExportAttribute), false) && !field.IsDefined(typeof(LuaExportAttribute), false))
				{
					exportFields.Add(field);
				}
			}
			if ((ExportAttributeType.LUA & attrType) > 0)
			{
				if (field.IsDefined(typeof(LuaExportAttribute), false))
				{
					exportFields.Add(field);
				}
			}
		}
		//sort fields
		exportFields.Sort ((a, b) =>  {
			return string.Compare (a.Name, b.Name);
		});
		return exportFields;
	}
}
