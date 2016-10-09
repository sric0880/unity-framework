// Auto generated code
using System.IO;

public class Arr_ConfTestArray1_Serializer
{
	public static ConfTestArray1[] Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestArray1[] d = null;
		int size = o.ReadInt32();
		d = new ConfTestArray1[size];
		for(int i = 0; i < size; ++i)
		{
			d[i] = ConfTestArray1_Serializer.Read(o);
		}
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestArray1[] d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Length;
		o.Write(size);
		for(int i = 0; i < size; ++i)
		{
			ConfTestArray1_Serializer.Write(o, d[i]);
		}
	}
}
