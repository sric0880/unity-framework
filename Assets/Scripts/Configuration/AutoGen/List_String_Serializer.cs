// Auto generated code
using System;
using System.Collections.Generic;
using System.IO;

public class List_String_Serializer
{
	public static List<string> Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		List<string> d = null;
		int size = o.ReadInt32();
		d = new List<string>(size);
		for(int i = 0; i < size; ++i)
		{
			string elem;
			elem = String_Serializer.Read(o);
			d.Add(elem);
		}
		return d;
	}

	public static void Write(BinaryWriter o, List<string> d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Count;
		o.Write(size);
		for(int i = 0; i < size; ++i)
		{
			String_Serializer.Write(o, d[i]);
		}
	}
}
