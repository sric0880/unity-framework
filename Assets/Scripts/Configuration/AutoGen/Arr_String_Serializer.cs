// Auto generated code
using System;
using System.IO;

public class Arr_String_Serializer
{
	public static String[] Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		String[] d = null;
		int size = o.ReadInt32();
		d = new string[size];
		for(int i = 0; i < size; ++i)
		{
			d[i] = String_Serializer.Read(o);
		}
		return d;
	}

	public static void Write(BinaryWriter o, String[] d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Length;
		o.Write(size);
		for(int i = 0; i < size; ++i)
		{
			String_Serializer.Write(o, d[i]);
		}
	}
}
