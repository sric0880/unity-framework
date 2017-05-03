// Auto generated code
using System;
using System.IO;

public class ConfBoot_Serializer
{
	public static ConfBoot Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfBoot d = new ConfBoot();
			d.locale = String_Serializer.Read(o);
			d.log_priority = o.ReadInt32();
			d.show_console = o.ReadBoolean();
		return d;
	}

	public static void Write(BinaryWriter o, ConfBoot d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			String_Serializer.Write(o, d.locale);
			o.Write(d.log_priority);
			o.Write(d.show_console);
	}
}
