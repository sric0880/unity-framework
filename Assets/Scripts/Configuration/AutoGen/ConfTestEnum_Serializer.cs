// Auto generated code
using System;
using System.IO;

public class ConfTestEnum_Serializer
{
	public static ConfTestEnum Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestEnum d = new ConfTestEnum();
			d.arrays = Arr_Int32_Serializer.Read(o);
			d.integer = o.ReadInt32();
			d.moduelsEnums2 = Arr_Modules_Serializer.Read(o);
			d.modulesEnums = Arr_Modules_Serializer.Read(o);
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestEnum d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			Arr_Int32_Serializer.Write(o, d.arrays);
			o.Write(d.integer);
			Arr_Modules_Serializer.Write(o, d.moduelsEnums2);
			Arr_Modules_Serializer.Write(o, d.modulesEnums);
	}
}
