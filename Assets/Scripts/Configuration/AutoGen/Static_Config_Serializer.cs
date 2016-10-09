// Auto generated code
using System;
using System.Collections.Generic;
using System.IO;

public class Static_Config_Serializer
{
	public static void Read(BinaryReader o)
	{
		Config.araysofarrays = ConfTestArray2_Serializer.Read(o);
		Config.confenum = ConfTestEnum_Serializer.Read(o);
		Config.confprimitive = ConfTestPrimitive_Serializer.Read(o);
		Config.dim2Array = Arr_Arr_Int32_Serializer.Read(o);
		Config.dim3Array = Arr_Arr_Int32_Serializer.Read(o);
		Config.listoflist = List_ConfTestListAndDict_Serializer.Read(o);
		Config.module = (Modules)o.ReadInt32();
		Config.money = Arr_Double_Serializer.Read(o);
		Config.strings = ConfTestStrings_Serializer.Read(o);
		Config.testEnumDict = Dictionary_Modules_ConfTestDict1_Serializer.Read(o);
		Config.testEnumList = List_Modules_Serializer.Read(o);
		Config.testFloatList = List_Single_Serializer.Read(o);
		Config.testIntDict = Dictionary_Int32_ConfTestDict_Serializer.Read(o);
		Config.testIntList = List_Int32_Serializer.Read(o);
		Config.testlistAndDict = ConfTestListAndDict_Serializer.Read(o);
		Config.testprivate = ConfTestPrivate_Serializer.Read(o);
		Config.testStrDict = Dictionary_String_ConfTestDict2_Serializer.Read(o);
		Config.testStrList = List_String_Serializer.Read(o);
		Config.teststruts = ConfTestStruct_Serializer.Read(o);
		Config.words = Arr_String_Serializer.Read(o);
	}

	public static void Write(BinaryWriter o)
	{
		ConfTestArray2_Serializer.Write(o, Config.araysofarrays);
		ConfTestEnum_Serializer.Write(o, Config.confenum);
		ConfTestPrimitive_Serializer.Write(o, Config.confprimitive);
		Arr_Arr_Int32_Serializer.Write(o, Config.dim2Array);
		Arr_Arr_Int32_Serializer.Write(o, Config.dim3Array);
		List_ConfTestListAndDict_Serializer.Write(o, Config.listoflist);
		o.Write((int)Config.module);
		Arr_Double_Serializer.Write(o, Config.money);
		ConfTestStrings_Serializer.Write(o, Config.strings);
		Dictionary_Modules_ConfTestDict1_Serializer.Write(o, Config.testEnumDict);
		List_Modules_Serializer.Write(o, Config.testEnumList);
		List_Single_Serializer.Write(o, Config.testFloatList);
		Dictionary_Int32_ConfTestDict_Serializer.Write(o, Config.testIntDict);
		List_Int32_Serializer.Write(o, Config.testIntList);
		ConfTestListAndDict_Serializer.Write(o, Config.testlistAndDict);
		ConfTestPrivate_Serializer.Write(o, Config.testprivate);
		Dictionary_String_ConfTestDict2_Serializer.Write(o, Config.testStrDict);
		List_String_Serializer.Write(o, Config.testStrList);
		ConfTestStruct_Serializer.Write(o, Config.teststruts);
		Arr_String_Serializer.Write(o, Config.words);
	}
}
