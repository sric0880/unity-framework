// Auto generated code
using System;
using System.IO;
using System.Collections.Generic;
using UF.Config.Attr;

namespace UF.Config
{
    public static class Dict_int_ConfHero
    {
    	public static void Serialize(BinaryWriter o, Dictionary<int, ConfHero> d)
    	{
    	    int size = d.Count;
    	    o.Write(size);
    	    foreach(var elem in d)
    	    {
    	        o.Write(elem.Key);
    	        elem.Value.Serialize(o);
    	    }
    	}

    	public static Dictionary<int, ConfHero> Deserialize(BinaryReader o)
    	{
    		Dictionary<int, ConfHero> d = new Dictionary<int, ConfHero>();
    		int size = o.ReadInt32();
    		for(int i = 0; i < size; ++i)
    		{
    		    int key;
    		    key = o.ReadInt32();
    		    ConfHero value;
    		    value = new ConfHero(); value.Deserialize(o);
    		    d.Add(key, value);
    		}
    		return d;
    	}
    }
}
