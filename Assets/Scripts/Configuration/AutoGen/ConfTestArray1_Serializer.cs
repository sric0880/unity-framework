// Auto generated code
using System;
using System.IO;

public class ConfTestArray1_Serializer
{
	public static ConfTestArray1 Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestArray1 d = new ConfTestArray1();
			d.address = String_Serializer.Read(o);
			d.random_port = Arr_Int32_Serializer.Read(o);
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestArray1 d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			String_Serializer.Write(o, d.address);
			Arr_Int32_Serializer.Write(o, d.random_port);
	}
}
