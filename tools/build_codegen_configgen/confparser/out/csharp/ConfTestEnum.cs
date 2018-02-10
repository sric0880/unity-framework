// Auto generated code
using System;
using System.IO;
using System.Collections.Generic;
using UF.Config.Attr;

namespace UF.Config
{
    public class ConfTestEnum : ISerializable {
        public int integer;

        public int[] arrays;

        public Modules[] modulesEnums;

        public Modules[] moduelsEnums2;

        public void Serialize(BinaryWriter o)
        {
            o.Write(integer);
            Array_int.Serialize(o, arrays);
            Array_Modules.Serialize(o, modulesEnums);
            Array_Modules.Serialize(o, moduelsEnums2);
        }

        public void Deserialize(BinaryReader o)
        {
            integer = o.ReadInt32();
            arrays = Array_int.Deserialize(o);
            modulesEnums = Array_Modules.Deserialize(o);
            moduelsEnums2 = Array_Modules.Deserialize(o);
        }
    }
}
