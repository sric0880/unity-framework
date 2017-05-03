using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using FullSerializer;

public static class CodegenExe {

	public static Type[] staticTypes = { typeof(Config), typeof(LaunchConfig) };
	public static string[] typeNames = { "conf", "launch_conf" };
	// for writing json file
	private static Dictionary<string, LocaleJsonObject> localeDict = new Dictionary<string, LocaleJsonObject>();

	public static void Main(string[] args)
	{
		string autoGenPath = args[0];
		string exampleConfigPath = args[1];
		string luaExportDir = args[2];
		Console.WriteLine(autoGenPath);
		BinarySerializerCodeGenerator gen = new BinarySerializerCodeGenerator();
		for (int i = 0; i < staticTypes.Length; ++i)
		{
			gen.GenCode(staticTypes[i], autoGenPath);
		}
		gen.WriteTypeMd5File(autoGenPath);
		gen.WriteLuaEnumsFile(luaExportDir);

		SaveConfigAsJson(exampleConfigPath);
	}

	private static void SaveConfigAsJson(string exampleConfigPath)
	{
		localeDict.Add("LOCALE_ID", new LocaleJsonObject());
		for (int i = 0; i < staticTypes.Length; ++i)
		{
			var type = staticTypes[i];
			string folder = Path.Combine(exampleConfigPath, typeNames[i]);
			FileUtils.CreateDirectoryIfNeed(folder);
			WriteConfigAsJson(type, folder);
		}
	}

	private static readonly fsSerializer _serializer = new fsSerializer();

	private static void WriteConfigAsJson(Type type, string folder)
	{
		var exportFields = ClassFieldFilter.GetConfigFieldInfo(type, ExportAttributeType.CS|ExportAttributeType.LUA);
		if (exportFields.Count == 0)
		{
			return;
		}
		_serializer.Config.CustomDateTimeFormatString = "G";
		_serializer.AddConverter(new MyCustomEnumConverter());
		foreach (var field in exportFields)
		{
			if (field.GetValue(null) == null)
			{
				var value = InstanceUtility.InstanceOfType(field.FieldType);
				field.SetValue(null, value);
			}
			var exportAttr = TypeUtility.GetCustomAttribute<ExportAttribute>(field);
			exportAttr.ValidateType(field.FieldType);
			string jsonFilename = string.IsNullOrEmpty(exportAttr.Name) ? field.Name : exportAttr.Name;
			var file = Path.Combine(folder, jsonFilename + ".json");
			fsData data;
			_serializer.TrySerialize(field.FieldType, field.GetValue(null), out data).AssertSuccess();
			WriteDataToJson(file, data);
			if (HasLocaleAttribute(field.FieldType))
			{
				file = Path.Combine(folder, jsonFilename + ".locale.json");
				_serializer.TrySerialize(typeof(Dictionary<string, LocaleJsonObject>), localeDict, out data).AssertSuccess();
				WriteDataToJson(file, data);
			}
		}
	}

	private static void WriteDataToJson(string file, fsData data)
	{
		if (File.Exists(file))
		{
			File.Delete(file);
		}
		using (FileStream fs = File.OpenWrite(file))
		{
			byte[] content = new UTF8Encoding(true).GetBytes(fsJsonPrinter.PrettyJson(data));
			fs.Write(content, 0, content.Length);
		}
	}

	private static bool HasLocaleAttribute(Type type)
	{
		foreach (var field in TypeUtilityEditorOnly.GetTypeReferencedFieldinfos(type))
		{
			var attr = TypeUtility.GetCustomAttribute<LocaleAttribute>(field);
			if (attr != null) return true;
		}
		return false;
	}
}
