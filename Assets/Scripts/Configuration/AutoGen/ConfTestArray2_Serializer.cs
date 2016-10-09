// Auto generated code
using System.IO;

public class ConfTestArray2_Serializer
{
	public static ConfTestArray2 Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestArray2 d = new ConfTestArray2();
			d.server_list = Arr_ConfTestArray_Serializer.Read(o);
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestArray2 d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			Arr_ConfTestArray_Serializer.Write(o, d.server_list);
	}
}
