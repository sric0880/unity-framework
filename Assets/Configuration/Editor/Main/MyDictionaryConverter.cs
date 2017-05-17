using System;
using System.Collections;
using System.Collections.Generic;
using FullSerializer;

// While the generic IEnumerable converter can handle dictionaries, we
// process them separately here because we support a few more advanced
// use-cases with dictionaries, such as inline strings. Further, dictionary
// processing in general is a bit more advanced because a few of the
// collection implementations are buggy.
public class MyDictionaryConverter : FullSerializer.Internal.fsDictionaryConverter 
{
	public override fsResult TryDeserialize(fsData data, ref object instance_, Type storageType)
	{
		var instance = (IDictionary)instance_;
		var result = fsResult.Success;

		Type keyStorageType, valueStorageType;
		GetKeyValueTypes(instance.GetType(), out keyStorageType, out valueStorageType);
		string idFieldName = IDAttribute.TypeHasIDAttr(valueStorageType);

		if (data.IsList)
		{
			var list = data.AsList;
			for (int i = 0; i < list.Count; ++i)
			{
				var item = list[i];

				fsData keyData, valueData;
				if ((result += CheckType(item, fsDataType.Object)).Failed) return result;
				if ((result += CheckKey(item, "Key", out keyData)).Failed) return result;
				if ((result += CheckKey(item, "Value", out valueData)).Failed) return result;
				if (idFieldName != null)
				{
					valueData.AsDictionary.Add(idFieldName, keyData);
				}

				object keyInstance = null, valueInstance = null;
				if ((result += Serializer.TryDeserialize(keyData, keyStorageType, ref keyInstance)).Failed) return result;
				if ((result += Serializer.TryDeserialize(valueData, valueStorageType, ref valueInstance)).Failed) return result;

				if ((result += AddItemToDictionary(instance, keyInstance, valueInstance)).Failed) return result;
			}
		}
		else {
			return FailExpectedType(data, fsDataType.Array, fsDataType.Object);
		}

		return result;
	}

	public override fsResult TrySerialize(object instance_, out fsData serialized, Type storageType)
	{
		serialized = fsData.Null;

		var result = fsResult.Success;

		var instance = (IDictionary)instance_;

		Type keyStorageType, valueStorageType;
		GetKeyValueTypes(instance.GetType(), out keyStorageType, out valueStorageType);
		string idFieldName = IDAttribute.TypeHasIDAttr(valueStorageType);

		// No other way to iterate dictionaries and still have access to the
		// key/value info
		IDictionaryEnumerator enumerator = instance.GetEnumerator();

		var serializedKeys = new List<fsData>(instance.Count);
		var serializedValues = new List<fsData>(instance.Count);
		while (enumerator.MoveNext())
		{
			fsData keyData, valueData;
			if ((result += Serializer.TrySerialize(keyStorageType, enumerator.Key, out keyData)).Failed) return result;
			if ((result += Serializer.TrySerialize(valueStorageType, enumerator.Value, out valueData)).Failed) return result;

			serializedKeys.Add(keyData);
			serializedValues.Add(valueData);
		}

		serialized = fsData.CreateList(serializedKeys.Count);
		var serializedList = serialized.AsList;

		for (int i = 0; i < serializedKeys.Count; ++i)
		{
			fsData key = serializedKeys[i];
			fsData value = serializedValues[i];

			if (idFieldName != null)
			{
				value.AsDictionary.Remove(idFieldName);
			}

			var container = new Dictionary<string, fsData>();
			container["Key"] = key;
			container["Value"] = value;
			serializedList.Add(new fsData(container));
		}

		return result;
	}
}
