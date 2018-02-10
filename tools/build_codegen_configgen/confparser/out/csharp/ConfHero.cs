// Auto generated code
using System;
using System.IO;
using System.Collections.Generic;
using UF.Config.Attr;

namespace UF.Config
{
    public class ConfHero : ISerializable {
        [ID]
        public int[] attack_sections;

        public float[] attack_wait_time_min;
        [Locale]
        public float[] attack_wait_time_max;

        public float[] skill_wait_time;

        public float attack_weight;

        public float[] skill_weight;
        [Locale]
        public bool test_add;

        public void Serialize(BinaryWriter o)
        {
            Array_int.Serialize(o, attack_sections);
            Array_float.Serialize(o, attack_wait_time_min);
            Array_float.Serialize(o, attack_wait_time_max);
            Array_float.Serialize(o, skill_wait_time);
            o.Write(attack_weight);
            Array_float.Serialize(o, skill_weight);
            o.Write(test_add);
        }

        public void Deserialize(BinaryReader o)
        {
            attack_sections = Array_int.Deserialize(o);
            attack_wait_time_min = Array_float.Deserialize(o);
            attack_wait_time_max = Array_float.Deserialize(o);
            skill_wait_time = Array_float.Deserialize(o);
            attack_weight = o.ReadSingle();
            skill_weight = Array_float.Deserialize(o);
            test_add = o.ReadBoolean();
        }
    }
}
