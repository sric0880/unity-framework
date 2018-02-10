// Auto generated code
using System;
using System.IO;
using System.Collections.Generic;
using UF.Config.Attr;

namespace UF.Config
{
    public static class Array_Modules
    {
    	public static void Serialize(BinaryWriter o, Modules[] d)
    	{
    	    int size = d.Length;
    	    o.Write(size);
    	    for(int i = 0; i < size; ++i)
    	    {
    	        o.Write((int)d[i]);
    	    }
    	}

    	public static Modules[] Deserialize(BinaryReader o)
    	{
    		int size = o.ReadInt32();
    		Modules[] d = new Modules[size];
    		for(int i = 0; i < size; ++i)
    		{
    		    d[i] = (Modules)o.ReadInt32();
    		}
    		return d;
    	}
    }
}
