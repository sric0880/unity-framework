// Auto generated code
using System.IO;

public class Static_LaunchConfig_Serializer
{
	public static void Read(BinaryReader o)
	{
		LaunchConfig.boot = ConfBoot_Serializer.Read(o);
		LaunchConfig.update = ConfUpdate_Serializer.Read(o);
	}

	public static void Write(BinaryWriter o)
	{
		ConfBoot_Serializer.Write(o, LaunchConfig.boot);
		ConfUpdate_Serializer.Write(o, LaunchConfig.update);
	}
}
