// Auto generated code
using System;
using System.IO;

public class ConfUpdate_Serializer
{
	public static ConfUpdate Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfUpdate d = new ConfUpdate();
			d.auto_update_res = o.ReadBoolean();
			d.hosts = Arr_String_Serializer.Read(o);
		return d;
	}

	public static void Write(BinaryWriter o, ConfUpdate d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			o.Write(d.auto_update_res);
			Arr_String_Serializer.Write(o, d.hosts);
	}
}
