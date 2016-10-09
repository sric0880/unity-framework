// Auto generated code
using System;
using System.IO;

public class ConfTestPrivate_Serializer
{
	public static ConfTestPrivate Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestPrivate d = new ConfTestPrivate();
			d.yes = o.ReadInt32();
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestPrivate d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			o.Write(d.yes);
	}
}
