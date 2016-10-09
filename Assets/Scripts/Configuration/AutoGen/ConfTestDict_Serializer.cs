// Auto generated code
using System;
using System.IO;

public class ConfTestDict_Serializer
{
	public static ConfTestDict Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestDict d = new ConfTestDict();
			d.carrer_icon_path = String_Serializer.Read(o);
			d.desc = String_Serializer.Read(o);
			d.hero_id = o.ReadInt32();
			d.id = o.ReadInt32();
			d.isMale = o.ReadBoolean();
			d.model_female = Arr_String_Serializer.Read(o);
			d.model_male = Arr_String_Serializer.Read(o);
			d.name = String_Serializer.Read(o);
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestDict d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			String_Serializer.Write(o, d.carrer_icon_path);
			String_Serializer.Write(o, d.desc);
			o.Write(d.hero_id);
			o.Write(d.id);
			o.Write(d.isMale);
			Arr_String_Serializer.Write(o, d.model_female);
			Arr_String_Serializer.Write(o, d.model_male);
			String_Serializer.Write(o, d.name);
	}
}
