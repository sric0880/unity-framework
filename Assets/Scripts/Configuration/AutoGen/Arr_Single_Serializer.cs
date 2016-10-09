// Auto generated code
using System;
using System.IO;

public class Arr_Single_Serializer
{
	public static Single[] Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		Single[] d = null;
		int size = o.ReadInt32();
		d = new Single[size];
		for(int i = 0; i < size; ++i)
		{
			d[i] = o.ReadSingle();
		}
		return d;
	}

	public static void Write(BinaryWriter o, Single[] d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Length;
		o.Write(size);
		for(int i = 0; i < size; ++i)
		{
			o.Write(d[i]);
		}
	}
}
