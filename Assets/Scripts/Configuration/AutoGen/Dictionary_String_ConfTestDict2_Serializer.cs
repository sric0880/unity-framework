// Auto generated code
using System;
using System.Collections.Generic;
using System.IO;

public class Dictionary_String_ConfTestDict2_Serializer
{
	public static Dictionary<string, ConfTestDict2> Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		Dictionary<string, ConfTestDict2> d = null;
		int size = o.ReadInt32();
		d = new Dictionary<string, ConfTestDict2>(size);
		for(int i = 0; i < size; ++i)
		{
			string key;
			key = String_Serializer.Read(o);
			ConfTestDict2 value;
			value = ConfTestDict2_Serializer.Read(o);
			d.Add(key, value);
		}
		return d;
	}

	public static void Write(BinaryWriter o, Dictionary<string, ConfTestDict2> d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Count;
		o.Write(size);
		foreach(var elem in d)
		{
			String_Serializer.Write(o, elem.Key);
			ConfTestDict2_Serializer.Write(o, elem.Value);
		}
	}
}
