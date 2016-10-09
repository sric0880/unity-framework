// Auto generated code
using System.IO;

public class Arr_ConfTestArray_Serializer
{
	public static ConfTestArray[] Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestArray[] d = null;
		int size = o.ReadInt32();
		d = new ConfTestArray[size];
		for(int i = 0; i < size; ++i)
		{
			d[i] = ConfTestArray_Serializer.Read(o);
		}
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestArray[] d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Length;
		o.Write(size);
		for(int i = 0; i < size; ++i)
		{
			ConfTestArray_Serializer.Write(o, d[i]);
		}
	}
}
