// Auto generated code
using System;
using System.IO;

public class ConfTestDict1_Serializer
{
	public static ConfTestDict1 Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestDict1 d = new ConfTestDict1();
			d.finish_dungeon_id = o.ReadInt32();
			d.military_rank_level = o.ReadInt32();
			d.module = (Modules)o.ReadInt32();
			d.role_level = o.ReadInt32();
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestDict1 d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			o.Write(d.finish_dungeon_id);
			o.Write(d.military_rank_level);
			o.Write((int)d.module);
			o.Write(d.role_level);
	}
}
