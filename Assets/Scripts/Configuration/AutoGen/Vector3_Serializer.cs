// Auto generated code
using System;
using System.IO;
using UnityEngine;

public class Vector3_Serializer
{
	public static Vector3 Read(BinaryReader o)
	{
		
		Vector3 d = new Vector3();
			d.x = o.ReadSingle();
			d.y = o.ReadSingle();
			d.z = o.ReadSingle();
		return d;
	}

	public static void Write(BinaryWriter o, Vector3 d)
	{
		
			o.Write(d.x);
			o.Write(d.y);
			o.Write(d.z);
	}
}
