// Auto generated code
using System;
using System.Collections.Generic;
using System.IO;

public class ConfTestListAndDict_Serializer
{
	public static ConfTestListAndDict Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestListAndDict d = new ConfTestListAndDict();
			d.heroes = Dictionary_String_ConfHero_Serializer.Read(o);
			d.id = o.ReadInt32();
			d.levels = List_Int32_Serializer.Read(o);
			d.listofHeros = List_ConfHero_Serializer.Read(o);
			d.names = Dictionary_Int32_String_Serializer.Read(o);
			d.stars = List_String_Serializer.Read(o);
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestListAndDict d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			Dictionary_String_ConfHero_Serializer.Write(o, d.heroes);
			o.Write(d.id);
			List_Int32_Serializer.Write(o, d.levels);
			List_ConfHero_Serializer.Write(o, d.listofHeros);
			Dictionary_Int32_String_Serializer.Write(o, d.names);
			List_String_Serializer.Write(o, d.stars);
	}
}
