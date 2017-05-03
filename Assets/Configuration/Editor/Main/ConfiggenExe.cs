using System;
using System.IO;
using System.Text;
using System.Reflection;
using FullSerializer;

public static class ConfiggenExe
{
	public static Type[] staticTypes = { typeof(Config), typeof(LaunchConfig) };
	public static string[] typeNames = { "conf", "launch_conf" };
	private static readonly fsSerializer _serializer = new fsSerializer();

	public static void Main(string[] args)
	{
		string exportedJsonFolder = args[0];
		string exportBinFolder = args[1];
		ReadConfigAsJson(exportedJsonFolder);
		SaveConfigAsBin(exportBinFolder);
	}

	private static void ReadConfigAsJson(string exportedJsonFolder)
	{
		for (int i = 0; i < ConfiggenExe.staticTypes.Length; ++i)
		{
			string folder = Path.Combine(exportedJsonFolder, typeNames[i]);
			ReadConfigAsJson(ConfiggenExe.staticTypes[i], folder);
			TypeUtilityEditorOnly.Validate(ConfiggenExe.staticTypes[i]);
		}
	}

	private static void SaveConfigAsBin(string exportBinFolder)
	{
		for (int i = 0; i < ConfiggenExe.staticTypes.Length; ++i)
		{
			WriteConfigAsBin(ConfiggenExe.staticTypes[i], ConfiggenExe.typeNames[i], exportBinFolder);
		}
	}

	private static void ReadConfigAsJson(Type type, string folder)
	{
		var exportFields = ClassFieldFilter.GetConfigFieldInfo(type);
		if (exportFields.Count == 0)
		{
			return;
		}
		_serializer.AddConverter(new MyCustomEnumConverter());
		foreach (var field in exportFields)
		{
			var exportAttr = TypeUtility.GetCustomAttribute<ExportAttribute>(field);
			string jsonFilename = string.IsNullOrEmpty(exportAttr.Name) ? field.Name : exportAttr.Name;
			var file = Path.Combine(folder, jsonFilename + ".json");
			if (!File.Exists(file))
			{
				throw new Exception(string.Format("xlsx file {0} not exists", jsonFilename));
			}
			Console.WriteLine("read from config: " + jsonFilename);
			FileStream fs = File.Open(file, FileMode.Open);
			StringBuilder sb = new StringBuilder();
			byte[] b = new byte[1024];
			UTF8Encoding temp = new UTF8Encoding(true);

			while (fs.Read(b, 0, b.Length) > 0)
			{
				sb.Append(temp.GetString(b));
			}
			fs.Close();

			fsData data;
			fsResult res = fsJsonParser.Parse(sb.ToString(), out data);
			res.AssertSuccess();
			var value = field.GetValue(null);
			_serializer.TryDeserialize(data, field.FieldType, ref value).AssertSuccess();
			field.SetValue(null, value);
		}
	}

	private static void WriteConfigAsBin(Type type, string name, string folder)
	{
		var gen = BinarySerializerCodeGenerator.GetGenerator(type);
		string serializerFileName = gen.SerializerFileName(type);
		if (!serializerFileName.StartsWith("Static_"))
		{
			throw new Exception("Config type must be static");
		}

		string revisionFilePath = Path.Combine(folder, name + ".rev");
		string md5 = TypesMd5.typeMd5[serializerFileName];
		using (StreamWriter sw = new StreamWriter(File.OpenWrite(revisionFilePath)))
		{
			sw.WriteLine("md5:" + md5);
		}

		Type serializer = Type.GetType(serializerFileName);
		if (serializer != null)
		{
			MethodInfo write = serializer.GetMethod("Write", BindingFlags.Public | BindingFlags.Static);
			if (write != null)
			{
				string path = Path.Combine(folder, name + ".bin");
				BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create));
				bw.Write(md5);
				write.Invoke(null, new object[] { bw });
				bw.Close();
			}
			else
				throw new Exception("Generate serializer code first");
		}
		else
			throw new Exception("Generate serializer code first");
	}
}
