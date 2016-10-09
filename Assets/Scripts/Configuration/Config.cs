using UnityEngine;
using System.Collections.Generic;

public static class Config {

	[Export]public static ConfTestEnum confenum;
	[Export]public static ConfTestPrimitive confprimitive;

	[Export]public static Modules module;

	[Export]public static string[] words = { };
	[Export]public static double[] money = { };
	[Export]public static int[][] dim2Array;
	[Export]public static int[][] dim3Array;

	[Export]public static ConfTestArray2 araysofarrays;

	[Export]public static ConfTestStrings strings;

	[Export]public static ConfTestStruct teststruts;

	[Export]public static ConfTestPrivate testprivate;

	[Export]public static Dictionary<int, ConfTestDict> testIntDict;

	[Export]public static Dictionary<string, ConfTestDict2> testStrDict;

	[Export]public static Dictionary<Modules, ConfTestDict1> testEnumDict;

	[Export]public static ConfTestListAndDict testlistAndDict;

	[Export]public static List<int> testIntList;

	[Export]public static List<string> testStrList;

	[Export]public static List<Modules> testEnumList;

	[Export]public static List<float> testFloatList;

	[Export]public static List<ConfTestListAndDict> listoflist;
}
