// Auto generated code
using System;
using System.IO;
using System.Collections.Generic;
using UF.Config.Attr;

namespace UF.Config
{
    public static class Array_int
    {
    	public static void Serialize(BinaryWriter o, int[] d)
    	{
    	    int size = d.Length;
    	    o.Write(size);
    	    for(int i = 0; i < size; ++i)
    	    {
    	        o.Write(d[i]);
    	    }
    	}

    	public static int[] Deserialize(BinaryReader o)
    	{
    		int size = o.ReadInt32();
    		int[] d = new int[size];
    		for(int i = 0; i < size; ++i)
    		{
    		    d[i] = o.ReadInt32();
    		}
    		return d;
    	}
    }
}
