// Auto generated code
using System;
using System.IO;

public class ConfTestArray_Serializer
{
	public static ConfTestArray Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestArray d = new ConfTestArray();
			d.address_list = Arr_ConfTestArray1_Serializer.Read(o);
			d.name = String_Serializer.Read(o);
			d.serverid = o.ReadInt32();
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestArray d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			Arr_ConfTestArray1_Serializer.Write(o, d.address_list);
			String_Serializer.Write(o, d.name);
			o.Write(d.serverid);
	}
}
