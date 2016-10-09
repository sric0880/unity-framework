using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Reflection;

public class InstanceUtility {

	public static object InstanceOfType(Type type)
	{
		if (type.IsEnum)
		{
			return InstanceOfEnum(type);
		}
		else if (type.IsPrimitive || type.IsValueType)
		{
			return Activator.CreateInstance(type);
		}
		else if (type == typeof(string))
		{
			return "";
		}
		else 
		{
			if (type.IsGenericType)
			{
				var typeDef = type.GetGenericTypeDefinition();
				if (typeDef == typeof(Dictionary<,>))
				{
					return InstanceOfDict(type);
				}
				else if (typeDef == typeof(List<>))
				{
					return InstanceOfList(type);

				}
				else
				{
					Debug.LogErrorFormat("Not Supported Type {0}", type);
					return null;
				}
			}
			else if (type.IsArray)
			{
				return InstanceOfArray(type);
			}
			else 
			{
				return InstanceOfClass(type);
			}
		}
	}

	private static object InstanceOfEnum(Type type)
	{
		Array values = Enum.GetValues(type);
		if (values.Length > 0)
		{
			return values.GetValue(0);
		}
		else
		{
			return Activator.CreateInstance(type);
		}
	}

	private static object InstanceOfDict(Type type)
	{
		var args = type.GetGenericArguments();
		var keyType = args[0];
		var valueType = args[1];
		var obj = Activator.CreateInstance(type);

		if (obj != null && obj is IDictionary)
		{
			var dict = obj as IDictionary;
			addKeyValuePair(dict, keyType, valueType);
		}
		else
		{
			Debug.LogErrorFormat("InstanceOfDict fail {0}", type);
		}
		return obj;
	}

	public static void addKeyValuePair(IDictionary dict, Type keyType, Type valueType)
	{
		if (dict == null) return;
		if (keyType == typeof(int))
		{
			int max = int.MinValue;
			foreach (var key in dict.Keys)
			{
				if ((int)key > max)
					max = (int)key;
			}
			int k = dict.Keys.Count == 0 ? 0 : max + 1; 
			dict.Add(k, InstanceOfType(valueType));
		}
		if (keyType == typeof(string))
		{
			TimeSpan diff = DateTime.UtcNow.Subtract(new DateTime(1970,1,1,0,0,0));
			dict.Add(diff.TotalSeconds.ToString(), InstanceOfType(valueType));
		}
		else if (keyType.IsEnum)
		{
			Array enumValues = Enum.GetValues(keyType);
			List<int> indexes = new List<int>(enumValues.Length);
			for (int i = 0; i < enumValues.Length; ++i)
				indexes.Add(i);
			foreach (var key in dict.Keys)
			{
				for (int j = 0; j < enumValues.Length; ++j)
				{
					if (enumValues.GetValue(j).Equals(key))
					{
						indexes.Remove(j);
						break;
					}
				}
			}
			if (indexes.Count == 0) return;
			else  dict.Add(enumValues.GetValue(indexes[0]), InstanceOfType(valueType));
		}
	}

	private static object InstanceOfList(Type type)
	{
		var valueType = type.GetGenericArguments()[0];
		var list = Activator.CreateInstance(type);
		if(list != null && list is IList)
		{
			var lst = list as IList;
			lst.Add(InstanceOfType(valueType));
		}
		else 
		{
			Debug.LogErrorFormat("InstanceOfList fail {0}", type);
		}
		return list;
	}

	private static object InstanceOfArray(Type type)
	{
		var valueType = type.GetElementType();
		var list = (Array)Activator.CreateInstance(type, 1);
		if(list != null)
		{
			list.SetValue(InstanceOfType(valueType), 0);
		}
		else 
		{
			Debug.LogErrorFormat("InstanceOfArray fail {0}", type);
		}
		return list;
	}

	private static object InstanceOfClass(Type type)
	{
		var obj = Activator.CreateInstance(type);
		if (obj != null)
		{
			foreach (var field in type.GetFields())
			{
				if (field.IsLiteral)
					continue;
				field.SetValue(obj, InstanceOfType(field.FieldType));
			}
		}
		else
		{
			Debug.LogErrorFormat("IntanceOfClass fail {0}", type);
		}
		return obj;
	}

	public static Array resizeArray(Array array, int newSize)
	{
		if (array == null || newSize  < 0) return null;
		var valueType = array.GetType().GetElementType();
		if (valueType == null) return null;

		Array newArr = Array.CreateInstance(valueType, newSize);
		for (int i = 0; i < newArr.Length; ++i)
		{
			if (i < array.Length)
			{
				newArr.SetValue(array.GetValue(i),i);
			}
			else 
			{
				newArr.SetValue(InstanceOfType(valueType), i );
			}
		}
		return newArr;
	}

	public static void resizeList(IList list, int newSize)
	{
		if (list == null || newSize < 0) return;
		var valueType = list.GetType().GetGenericArguments()[0];
		while (list.Count > newSize)
		{
			list.RemoveAt(list.Count - 1);
		}
		while (list.Count < newSize)
		{
			list.Add(InstanceOfType(valueType));
		}
	}
}
