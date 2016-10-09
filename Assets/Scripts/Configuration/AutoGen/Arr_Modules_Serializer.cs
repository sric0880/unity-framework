// Auto generated code
using System.IO;

public class Arr_Modules_Serializer
{
	public static Modules[] Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		Modules[] d = null;
		int size = o.ReadInt32();
		d = new Modules[size];
		for(int i = 0; i < size; ++i)
		{
			d[i] = (Modules)o.ReadInt32();
		}
		return d;
	}

	public static void Write(BinaryWriter o, Modules[] d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
		int size = d.Length;
		o.Write(size);
		for(int i = 0; i < size; ++i)
		{
			o.Write((int)d[i]);
		}
	}
}
