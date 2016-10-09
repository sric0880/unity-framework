// Auto generated code
using System;
using System.IO;

public class ConfTestDict2_Serializer
{
	public static ConfTestDict2 Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestDict2 d = new ConfTestDict2();
			d.finish_dungeon_id = o.ReadInt32();
			d.role_level = o.ReadInt32();
			d.stringId = String_Serializer.Read(o);
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestDict2 d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			o.Write(d.finish_dungeon_id);
			o.Write(d.role_level);
			String_Serializer.Write(o, d.stringId);
	}
}
