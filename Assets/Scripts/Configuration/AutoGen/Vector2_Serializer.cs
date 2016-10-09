// Auto generated code
using System;
using System.IO;
using UnityEngine;

public class Vector2_Serializer
{
	public static Vector2 Read(BinaryReader o)
	{
		
		Vector2 d = new Vector2();
			d.x = o.ReadSingle();
			d.y = o.ReadSingle();
		return d;
	}

	public static void Write(BinaryWriter o, Vector2 d)
	{
		
			o.Write(d.x);
			o.Write(d.y);
	}
}
