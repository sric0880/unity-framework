using System;
using System.Collections.Generic;
public static class TypesMd5
{ 
	public static Dictionary<string, string> typeMd5 = new Dictionary<string, string>();
	static TypesMd5()
	{
		typeMd5["Static_Config_Serializer"] = "4e918c29115eb6472a1fb2cfb8c2e6b5";
		typeMd5["Static_LaunchConfig_Serializer"] = "8282d6bc71e74b4a93e39aa97797d5d4";
		typeMd5["ConfBoot_Serializer"] = "29d29c65a1915bf74b8c64665a881949";
		typeMd5["String_Serializer"] = "58c08a57e1227f187fa58baefcdf27ff";
		typeMd5["ConfUpdate_Serializer"] = "fb6bf8b0d43499067fd19386c0bc8aa3";
		typeMd5["Arr_String_Serializer"] = "0d7a6db3f0d75623a300e085afea3ffd";

	}
}