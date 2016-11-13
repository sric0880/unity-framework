using System;
using System.Reflection;

public static class TypeUtility {

	public static string GetGenericTypeName(Type type)
	{
		if (!type.IsGenericType)
			throw new ArgumentException("type is not generic");
		return type.Name.Substring(0, type.Name.IndexOf("`"));
	}

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
}
