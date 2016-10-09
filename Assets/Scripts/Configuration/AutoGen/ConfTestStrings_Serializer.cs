// Auto generated code
using System;
using System.IO;

public class ConfTestStrings_Serializer
{
	public static ConfTestStrings Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestStrings d = new ConfTestStrings();
			d.fight_result = String_Serializer.Read(o);
			d.login = String_Serializer.Read(o);
			d.main_menu = String_Serializer.Read(o);
			d.new_card = String_Serializer.Read(o);
			d.start_film = String_Serializer.Read(o);
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestStrings d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			String_Serializer.Write(o, d.fight_result);
			String_Serializer.Write(o, d.login);
			String_Serializer.Write(o, d.main_menu);
			String_Serializer.Write(o, d.new_card);
			String_Serializer.Write(o, d.start_film);
	}
}
