// Auto generated code
using System;
using System.IO;

public class String_Serializer
{
	public static string Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		string d = null;
			d = o.ReadString();
		return d;
	}

	public static void Write(BinaryWriter o, string d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			o.Write(d);
	}
}
