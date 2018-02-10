using System;
using System.Reflection;
using System.Collections.Generic;

public static class TypeUtility {

	/// <summary>
	/// 获取Field、Property或无参数Method的返回值
	/// </summary>
	public static T GetValueEx<T>(this object obj, string name)
	{
		if (obj == null)
			return default(T);
		Type t = obj.GetType();
		FieldInfo field = t.GetField(name);
		if (field != null && typeof(T).IsAssignableFrom(field.FieldType))
			return (T)field.GetValue(obj);
		PropertyInfo prop = t.GetProperty(name);
		if (prop != null && prop.CanRead)
		{
			MethodInfo m = prop.GetGetMethod();
			if (m != null && typeof(T).IsAssignableFrom(m.ReturnType))
			{
				return (T)m.Invoke(obj, null);
			}
		}
		MethodInfo method = t.GetMethod(name, Type.EmptyTypes);
		if (method != null && typeof(T).IsAssignableFrom(method.ReturnType))
		{
			return (T)method.Invoke(obj, null);
		}
		return default(T);
	}
	
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
}
