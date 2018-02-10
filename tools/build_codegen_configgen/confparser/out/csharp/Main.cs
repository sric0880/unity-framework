// Auto generated code
using System;
using System.IO;
using System.Collections.Generic;
using UF.Config.Attr;

namespace UF.Config
{
    [Export("client")]
    public class Main : ISerializable {
        [XlsxName("E_测试枚举",11000)]
        [ID]
        [RefID]
        public ConfTestEnum confenum;

        public int inttest;
        [XlsxName("HeroesDict")]
        public Dictionary<int, ConfHero> testHeroes;

        public void Serialize(BinaryWriter o)
        {
            confenum.Serialize(o);
            o.Write(inttest);
            Dict_int_ConfHero.Serialize(o, testHeroes);
        }

        public void Deserialize(BinaryReader o)
        {
            confenum = new ConfTestEnum(); confenum.Deserialize(o);
            inttest = o.ReadInt32();
            testHeroes = Dict_int_ConfHero.Deserialize(o);
        }
    }
}
