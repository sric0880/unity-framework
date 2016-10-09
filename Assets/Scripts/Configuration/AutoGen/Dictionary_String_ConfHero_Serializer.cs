// Auto generated code
using System;
using System.Collections.Generic;
using System.IO;

public class Dictionary_String_ConfHero_Serializer
{
	public static Dictionary<string, ConfHero> Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		Dictionary<string, ConfHero> d = null;
		int size = o.ReadInt32();
		d = new Dictionary<string, ConfHero>(size);
		for(int i = 0; i < size; ++i)
		{
			string key;
			key = String_Serializer.Read(o);
			ConfHero value;
			value = ConfHero_Serializer.Read(o);
			d.Add(key, value);
		}
		return d;
	}

	public static void Write(BinaryWriter o, Dictionary<string, ConfHero> d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Count;
		o.Write(size);
		foreach(var elem in d)
		{
			String_Serializer.Write(o, elem.Key);
			ConfHero_Serializer.Write(o, elem.Value);
		}
	}
}
