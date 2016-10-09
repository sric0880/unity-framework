// Auto generated code
using System;
using System.IO;

public class ConfHero_Serializer
{
	public static ConfHero Read(BinaryReader o)
	{
		if(o.ReadBoolean() == false)
			return null;
		
		ConfHero d = new ConfHero();
			d.attack_sections = Arr_Int32_Serializer.Read(o);
			d.attack_wait_time_max = Arr_Single_Serializer.Read(o);
			d.attack_wait_time_min = Arr_Single_Serializer.Read(o);
			d.attack_weight = o.ReadSingle();
			d.skill_wait_time = Arr_Single_Serializer.Read(o);
			d.skill_weight = Arr_Single_Serializer.Read(o);
			d.test_add = o.ReadInt32();
		return d;
	}

	public static void Write(BinaryWriter o, ConfHero d)
	{
		o.Write(d != null);
		if(d == null)
			return;
		
			Arr_Int32_Serializer.Write(o, d.attack_sections);
			Arr_Single_Serializer.Write(o, d.attack_wait_time_max);
			Arr_Single_Serializer.Write(o, d.attack_wait_time_min);
			o.Write(d.attack_weight);
			Arr_Single_Serializer.Write(o, d.skill_wait_time);
			Arr_Single_Serializer.Write(o, d.skill_weight);
			o.Write(d.test_add);
	}
}
