// Auto generated code
using System.Collections.Generic;
using System.IO;

public class List_Modules_Serializer
{
	public static List<Modules> Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		List<Modules> d = null;
		int size = o.ReadInt32();
		d = new List<Modules>(size);
		for(int i = 0; i < size; ++i)
		{
			Modules elem;
			elem = (Modules)o.ReadInt32();
			d.Add(elem);
		}
		return d;
	}

	public static void Write(BinaryWriter o, List<Modules> d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Count;
		o.Write(size);
		for(int i = 0; i < size; ++i)
		{
			o.Write((int)d[i]);
		}
	}
}
