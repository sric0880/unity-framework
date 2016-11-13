using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using FullSerializer;

public static class ConfigWriter {

	private static readonly fsSerializer _serializer = new fsSerializer();

	public static void WriteConfigAsJson(Type type, string folder)
	{
		var exportFields = ClassFieldFilter.GetConfigFieldInfo(type);
		if (exportFields.Count == 0)
		{
			return;
		}
		
		foreach(var field in exportFields)
		{
			if (field.GetValue(null) == null)
			{
				UnityEngine.Debug.LogErrorFormat("Export {0} is null", field.Name);
				return;
			}
			var file = Path.Combine(folder, field.Name + ".json");
			fsData data;
			_serializer.TrySerialize(field.FieldType, field.GetValue(null), out data).AssertSuccess();
			if (File.Exists(file))
			{
				File.Delete(file);
			}
			using (FileStream fs = File.Create(file))
			{
				byte[] content = new UTF8Encoding(true).GetBytes(fsJsonPrinter.PrettyJson(data));
				fs.Write(content, 0, content.Length);
			}	
		}
	}



	public static void WriteConfigAsBin(Type type, string name, string folder)
	{
		var gen = BinarySerializerCodeGenerator.GetGenerator(type);
		string serializerFileName = gen.SerializerFileName(type);
		if (!serializerFileName.StartsWith("Static_"))
		{
			UnityEngine.Debug.LogError("Config type must be static");
			return;
		}

		byte[] md5 = Md5Utility.MD5(ConfigReader.configGeneratorMd5(type));

		Type serializer = Type.GetType(serializerFileName + ",Assembly-CSharp");
		if (serializer != null)
		{
			MethodInfo write = serializer.GetMethod("Write", BindingFlags.Public | BindingFlags.Static);
			if (write != null)
			{
				string path = Path.Combine(folder, name + ".conf");
				BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create));
				bw.Write(md5);
				write.Invoke(null, new object[]{bw});
				bw.Close();
			}
			else
				UnityEngine.Debug.LogError("Generate serializer code first");
		}
		else
			UnityEngine.Debug.LogError("Generate serializer code first");
	}
}
