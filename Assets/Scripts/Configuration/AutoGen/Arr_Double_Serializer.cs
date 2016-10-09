// Auto generated code
using System;
using System.IO;

public class Arr_Double_Serializer
{
	public static Double[] Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		Double[] d = null;
		int size = o.ReadInt32();
		d = new Double[size];
		for(int i = 0; i < size; ++i)
		{
			d[i] = o.ReadDouble();
		}
		return d;
	}

	public static void Write(BinaryWriter o, Double[] d)
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
