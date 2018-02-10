// Auto generated code
using System;
using System.IO;
using System.Collections.Generic;
using UF.Config.Attr;

namespace UF.Config
{
    public static class Array_float
    {
    	public static void Serialize(BinaryWriter o, float[] d)
    	{
    	    int size = d.Length;
    	    o.Write(size);
    	    for(int i = 0; i < size; ++i)
    	    {
    	        o.Write(d[i]);
    	    }
    	}

    	public static float[] Deserialize(BinaryReader o)
    	{
    		int size = o.ReadInt32();
    		float[] d = new float[size];
    		for(int i = 0; i < size; ++i)
    		{
    		    d[i] = o.ReadSingle();
    		}
    		return d;
    	}
    }
}
