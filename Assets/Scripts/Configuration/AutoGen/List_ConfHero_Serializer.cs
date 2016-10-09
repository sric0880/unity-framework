// Auto generated code
using System.Collections.Generic;
using System.IO;

public class List_ConfHero_Serializer
{
	public static List<ConfHero> Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		List<ConfHero> d = null;
		int size = o.ReadInt32();
		d = new List<ConfHero>(size);
		for(int i = 0; i < size; ++i)
		{
			ConfHero elem;
			elem = ConfHero_Serializer.Read(o);
			d.Add(elem);
		}
		return d;
	}

	public static void Write(BinaryWriter o, List<ConfHero> d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Count;
		o.Write(size);
		for(int i = 0; i < size; ++i)
		{
			ConfHero_Serializer.Write(o, d[i]);
		}
	}
}
