using System;
using System.Reflection;

public static class TypeUtility {

	public static string GetGenericTypeName(Type type)
	{
		if (!type.IsGenericType)
			throw new ArgumentException("type is not generic");
		return type.Name.Substring(0, type.Name.IndexOf("`"));
	}

	public static string GetStaticTypeName(Type type)
	{
		if (!(type.IsClass && type.IsAbstract && type.IsSealed))
			throw new ArgumentException("type is not static");
		return "Static_" + type.Name;
	}

	public static string GetSerializeTypeName(string typename)
	{
		return typename + "_Serializer";
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

	public static T GetCustomAttribute<T>(FieldInfo field) where T : Attribute
	{
		return (T)Attribute.GetCustomAttribute(field, typeof(T));
	}

	public static void ValidateAttributeType(FieldInfo field, string classTypeName)
	{
		var attrs = field.GetCustomAttributes(true) as Attribute[];
		if (attrs != null && attrs.Length > 0)
		{
			foreach (var attr in attrs)
			{
				if (attr is ValidateAttribute)
				{
					try
					{
						(attr as ValidateAttribute).ValidateType(field.FieldType);
					}
					catch (AttributeValidateException e)
					{
						e.classname = classTypeName;
						throw e;
					}
				}
			}
		}
	}

	public static void ValidateAttributeValue(FieldInfo field, object data, string classTypeName)
	{
		var attrs = field.GetCustomAttributes(true) as Attribute[];
		if (attrs != null && attrs.Length > 0)
		{
			foreach (var attr in attrs)
			{
				if (attr is ValidateAttribute)
				{
					try
					{
						(attr as ValidateAttribute).ValidateValue(field, data);
					}
					catch (AttributeValidateException e)
					{
						e.classname = classTypeName;
						throw e;
					}
				}
			}
		}
	}

	public static bool IsNumericType(Type type)
	{
		switch (Type.GetTypeCode(type))
		{
			case TypeCode.Byte:
			case TypeCode.SByte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
			case TypeCode.Decimal:
			case TypeCode.Double:
			case TypeCode.Single:
				return true;
			default:
				return false;
		}
	}

	public static bool IsIntegerType(Type type)
	{
		switch (Type.GetTypeCode(type))
		{
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				return true;
			default:
				return false;
		}
	}
}
