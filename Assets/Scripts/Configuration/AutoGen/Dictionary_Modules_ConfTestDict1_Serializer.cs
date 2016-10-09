// Auto generated code
using System.Collections.Generic;
using System.IO;

public class Dictionary_Modules_ConfTestDict1_Serializer
{
	public static Dictionary<Modules, ConfTestDict1> Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		Dictionary<Modules, ConfTestDict1> d = null;
		int size = o.ReadInt32();
		d = new Dictionary<Modules, ConfTestDict1>(size);
		for(int i = 0; i < size; ++i)
		{
			Modules key;
			key = (Modules)o.ReadInt32();
			ConfTestDict1 value;
			value = ConfTestDict1_Serializer.Read(o);
			d.Add(key, value);
		}
		return d;
	}

	public static void Write(BinaryWriter o, Dictionary<Modules, ConfTestDict1> d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Count;
		o.Write(size);
		foreach(var elem in d)
		{
			o.Write((int)elem.Key);
			ConfTestDict1_Serializer.Write(o, elem.Value);
		}
	}
}
