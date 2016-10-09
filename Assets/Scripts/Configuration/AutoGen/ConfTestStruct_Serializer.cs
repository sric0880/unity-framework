// Auto generated code
using System.IO;
using UnityEngine;

public class ConfTestStruct_Serializer
{
	public static ConfTestStruct Read(BinaryReader o)
	{
		
		ConfTestStruct d = new ConfTestStruct();
			d.coord = Vector2_Serializer.Read(o);
			d.pos = Vector3_Serializer.Read(o);
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestStruct d)
	{
		
			Vector2_Serializer.Write(o, d.coord);
			Vector3_Serializer.Write(o, d.pos);
	}
}
