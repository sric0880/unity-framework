// Auto generated code
using System;
using System.Collections.Generic;
using System.IO;

public class List_Single_Serializer
{
	public static List<Single> Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		List<Single> d = null;
		int size = o.ReadInt32();
		d = new List<Single>(size);
		for(int i = 0; i < size; ++i)
		{
			Single elem;
			elem = o.ReadSingle();
			d.Add(elem);
		}
		return d;
	}

	public static void Write(BinaryWriter o, List<Single> d)
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
