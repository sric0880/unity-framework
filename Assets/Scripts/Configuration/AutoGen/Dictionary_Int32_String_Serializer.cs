// Auto generated code
using System;
using System.Collections.Generic;
using System.IO;

public class Dictionary_Int32_String_Serializer
{
	public static Dictionary<Int32, string> Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		Dictionary<Int32, string> d = null;
		int size = o.ReadInt32();
		d = new Dictionary<Int32, string>(size);
		for(int i = 0; i < size; ++i)
		{
			Int32 key;
			key = o.ReadInt32();
			string value;
			value = String_Serializer.Read(o);
			d.Add(key, value);
		}
		return d;
	}

	public static void Write(BinaryWriter o, Dictionary<Int32, string> d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Count;
		o.Write(size);
		foreach(var elem in d)
		{
			o.Write(elem.Key);
			String_Serializer.Write(o, elem.Value);
		}
	}
}
