// Auto generated code
using System;
using System.IO;

public class ConfTestPrimitive_Serializer
{
	public static ConfTestPrimitive Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfTestPrimitive d = new ConfTestPrimitive();
			d.mBool = o.ReadBoolean();
			d.mByte = o.ReadByte();
			d.mDataTime = new DateTime(o.ReadInt64());
			d.mDec = Decimal_Serializer.Read(o);
			d.mDouble = o.ReadDouble();
			d.mFloat = o.ReadSingle();
			d.mInt = o.ReadInt32();
			d.mLong = o.ReadInt64();
			d.mShort = o.ReadInt16();
			d.mStr = String_Serializer.Read(o);
		return d;
	}

	public static void Write(BinaryWriter o, ConfTestPrimitive d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			o.Write(d.mBool);
			o.Write(d.mByte);
			o.Write(d.mDataTime.Ticks);
			Decimal_Serializer.Write(o, d.mDec);
			o.Write(d.mDouble);
			o.Write(d.mFloat);
			o.Write(d.mInt);
			o.Write(d.mLong);
			o.Write(d.mShort);
			String_Serializer.Write(o, d.mStr);
	}
}
