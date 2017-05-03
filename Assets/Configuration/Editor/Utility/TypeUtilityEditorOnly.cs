using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public static class TypeUtilityEditorOnly
{
	public static List<FieldInfo> GetTypeReferencedFieldinfos(Type type)
	{
		var gen = BinarySerializerCodeGenerator.GetGenerator(type);
		var refTypes = gen.TypeNameReferencedTypes(type);
		var classGenerator = new ClassGenerator();
		List<FieldInfo> ret = new List<FieldInfo>();
		foreach (var refType in refTypes)
		{
			if (classGenerator.Accept(refType))
			{
				ret.AddRange(ClassFieldFilter.GetClassFieldInfo(refType));
			}
		}
		return ret;
	}

	public static void Validate(Type configType)
	{
		var fields = ClassFieldFilter.GetConfigFieldInfo(configType);
		foreach (var field in fields)
		{
			object value = field.GetValue(null);
			Type valueType = value.GetType();
			Validate(value, valueType);
		}
	}

	private static void Validate(object data, Type type)
	{
		if (type == null)
			type = data != null ? data.GetType() : null;
		if (type.IsClass && !type.IsGenericType)
		{
			var fields = ClassFieldFilter.GetClassFieldInfo(type);
			foreach (var field in fields)
			{
				object value = field.GetValue(data);
				Type valueType = value.GetType();
				TypeUtility.ValidateAttributeValue(field, value, type.Name);
				Validate(value, valueType);
			}
		}
		if (type.IsArray)
		{
			var array = data as Array;
			var valueType = type.GetElementType();
			for (int i = 0; i < array.Length; ++i)
			{
				object value = array.GetValue(i);
				Validate(value, valueType);
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
					Validate(value, valueType);
				}
			}
			if (typeDef == typeof(Dictionary<,>))
			{
				var dict = data as IDictionary;
				var valueType = type.GetGenericArguments()[1];
				foreach (DictionaryEntry pair in dict)
				{
					var value = pair.Value;
					Validate(value, valueType);
				}
			}
		}
	}
}
