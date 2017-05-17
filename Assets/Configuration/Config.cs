using System.Collections.Generic;

/// <summary>
/// 用于导出配置
/// 每一个POD类中只允许使用一层数组或字典
/// 不支持数组或字典中嵌套数组或字典(其实代码是支持的，只不过导出Excel结构太复杂)
/// 
/// ValidateAttribute在lua导出表上无效，因为已经不是C#层来管理了
/// </summary>
public static class Config {

	[LuaExport("TestEnums")]
	public static ConfTestEnum confenum;
	[LuaExport("TestPrimitive")]
	public static ConfTestPrimitive confprimitive;
	[LuaExport("ServerList")]
	//[Export("F_服务器")]
	public static ConfTestArray[] server_list;
	//[Export("Z_字符串")]
	[LuaExport("Strings")]
	public static ConfTestStrings strings;
	//[Export("J_结构体")]
	[LuaExport("TestStruts")]
	public static ConfTestStruct teststruts;
	//[Export("C_测试私有成员")]
	[LuaExport("TestPrivate")]
	public static ConfTestPrivate testprivate;
	//[Export("Z_int字典", "id")]
	[LuaExport("IntDict", "id")]
	public static Dictionary<int, ConfTestDict> testIntDict;
	//[Export("Z_string字典", "stringId")]
	[LuaExport("StringDict", "stringId")]
	public static Dictionary<string, ConfTestDict2> testStrDict;
	//[Export("Z_enum字典", "module")]
	[LuaExport("EnumsDict", "module")]
	public static Dictionary<Modules, ConfTestDict1> testEnumDict;
	//[Export("Z_class字典")]
	[LuaExport("HeroesDict")]
	public static Dictionary<int, ConfHero> testHeroes;
	//[Export("L_list")]
	[LuaExport("L_list")]
	public static List<ConfTestListAndDict> listoflist;
	//[Export("L_listAndDict")]
	[LuaExport("L_listAndDict")]
	public static ConfTestListAndDict testlistAndDict;
	//[Export("L_listOfInt")]
	[LuaExport("L_listOfInt")]
	public static List<int> testIntList;
	//[Export("L_listOfString")]
	[LuaExport("L_listOfString")]
	public static List<string> testStrList;
	//[Export("L_list枚举")]
	[LuaExport("L_listOfEnums")]
	public static List<Modules> testEnumList;
	//[Export("L_listOfFloat")]
	[LuaExport("L_listOfFloat")]
	public static List<float> testFloatList;
}