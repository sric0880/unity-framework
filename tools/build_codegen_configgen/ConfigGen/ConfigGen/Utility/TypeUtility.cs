using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UF.Config.Attr;

namespace UF.Config
{
	public static class TypeUtility {

		public static T GetCustomAttribute<T>(FieldInfo field) where T : Attribute
		{
			return (T)Attribute.GetCustomAttribute(field, typeof(T));
		}

		public static bool HasAttribute<T>(Type type) where T : Attribute
		{
			if (type.IsClass && !type.IsGenericType)
			{
				var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
				foreach (var f in fields)
				{
					if (GetCustomAttribute<T>(f) != null)
					{
						return true;
					}
				}
			}
			if (type.IsArray)
			{
				var valueType = type.GetElementType();
				return HasAttribute<T>(valueType);
			}
			if (type.IsGenericType)
			{
				var typeDef = type.GetGenericTypeDefinition();
				if (typeDef == typeof(List<>))
				{
					var valueType = type.GetGenericArguments()[0];
					return HasAttribute<T>(valueType);
				}
				if (typeDef == typeof(Dictionary<,>))
				{
					var valueType = type.GetGenericArguments()[1];
					return HasAttribute<T>(valueType);
				}
			}
			return false;
		}

		public static void ValidateType(Type configType, Type type)
		{
			if (type.IsClass && !type.IsGenericType)
			{
				var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
				foreach (var field in fields)
				{
					ValidateTypeAttr(field, type.Name, configType);
					ValidateType(configType, field.FieldType);
				}
			}
			if (type.IsArray)
			{
				var valueType = type.GetElementType();
				ValidateType(configType, valueType);
			}
			if (type.IsGenericType)
			{
				var typeDef = type.GetGenericTypeDefinition();
				if (typeDef == typeof(List<>))
				{
					var valueType = type.GetGenericArguments()[0];
					ValidateType(configType, valueType);
				}
				if (typeDef == typeof(Dictionary<,>))
				{
					var valueType = type.GetGenericArguments()[1];
					ValidateType(configType, valueType);
				}
			}
		}

		public static void ValidateValue(object configData, object data, Type type)
		{
			if (data == null) return;
			type = type == null ? data.GetType() : type;
			if (type.IsClass && !type.IsGenericType)
			{
				var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
				foreach (var field in fields)
				{
					object value = field.GetValue(data);
					Type valueType = value.GetType();
					ValidateValueAttr(field, value, configData, type.Name);
					ValidateValue(configData, value, valueType);
				}
			}
			if (type.IsArray)
			{
				var array = data as Array;
				var valueType = type.GetElementType();
				for (int i = 0; i < array.Length; ++i)
				{
					object value = array.GetValue(i);
					ValidateValue(configData, value, valueType);
				}
			}
			if (type.IsGenericType)
			{
				var typeDef = type.GetGenericTypeDefinition();
				if (typeDef == typeof(List<>))
				{
					var list = data as IList;
					var valueType = type.GetGenericArguments()[0];
					for (int i = 0; i < list.Count; ++i)
					{
						var value = list[i];
						ValidateValue(configData, value, valueType);
					}
				}
				if (typeDef == typeof(Dictionary<,>))
				{
					var dict = data as IDictionary;
					var valueType = type.GetGenericArguments()[1];
					foreach (DictionaryEntry pair in dict)
					{
						var value = pair.Value;
						ValidateValue(configData, value, valueType);
					}
				}
			}
		}

		private static void ValidateValueAttr(FieldInfo field, object data, object configData, string classTypeName)
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
							(attr as ValidateAttribute).ValidateValue(field, data, configData);
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

		private static void ValidateTypeAttr(FieldInfo field, string classTypeName, Type configType)
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
							(attr as ValidateAttribute).ValidateType(configType, field.FieldType);
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
}