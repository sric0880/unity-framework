// Auto generated code
using System;
using System.Collections.Generic;
using System.IO;

public class List_Int32_Serializer
{
	public static List<Int32> Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		List<Int32> d = null;
		int size = o.ReadInt32();
		d = new List<Int32>(size);
		for(int i = 0; i < size; ++i)
		{
			Int32 elem;
			elem = o.ReadInt32();
			d.Add(elem);
		}
		return d;
	}

	public static void Write(BinaryWriter o, List<Int32> d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Count;
		o.Write(size);
		for(int i = 0; i < size; ++i)
		{
			o.Write(d[i]);
		}
	}
}
