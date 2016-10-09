// Auto generated code
using System;
using System.IO;

public class Arr_Arr_Int32_Serializer
{
	public static Int32[][] Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		Int32[][] d = null;
		int size = o.ReadInt32();
		d = new Int32[size][];
		for(int i = 0; i < size; ++i)
		{
			d[i] = Arr_Int32_Serializer.Read(o);
		}
		return d;
	}

	public static void Write(BinaryWriter o, Int32[][] d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Length;
		o.Write(size);
		for(int i = 0; i < size; ++i)
		{
			Arr_Int32_Serializer.Write(o, d[i]);
		}
	}
}
