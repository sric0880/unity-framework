// Auto generated code
using System.Collections.Generic;
using System.IO;

public class List_ConfTestListAndDict_Serializer
{
	public static List<ConfTestListAndDict> Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		List<ConfTestListAndDict> d = null;
		int size = o.ReadInt32();
		d = new List<ConfTestListAndDict>(size);
		for(int i = 0; i < size; ++i)
		{
			ConfTestListAndDict elem;
			elem = ConfTestListAndDict_Serializer.Read(o);
			d.Add(elem);
		}
		return d;
	}

	public static void Write(BinaryWriter o, List<ConfTestListAndDict> d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Count;
		o.Write(size);
		for(int i = 0; i < size; ++i)
		{
			ConfTestListAndDict_Serializer.Write(o, d[i]);
		}
	}
}
