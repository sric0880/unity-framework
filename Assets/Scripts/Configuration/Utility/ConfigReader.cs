using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using FullSerializer;

public static class ConfigReader {

	private static readonly fsSerializer _serializer = new fsSerializer();
	private static HashSet<Type> readTypes = new HashSet<Type>();

	public static void ReadConfigAsJson(Type type, string folder)
	{
		if (readTypes.Contains(type))
		{
			return;
		}
		else
		{
			readTypes.Add(type);
		}
		var exportFields = ClassFieldFilter.GetConfigFieldInfo(type);
		if (exportFields.Count == 0)
		{
			return;
		}
		foreach(var field in exportFields)
		{
			var file = Path.Combine(folder, field.Name + ".json");
			if (!File.Exists(file))
			{
				Debug.LogWarningFormat("Json file {0} not found", field.Name);
				continue;
			}
			FileStream fs = File.Open(file, FileMode.Open);
			StringBuilder sb = new StringBuilder();
			byte[] b = new byte[1024];
			UTF8Encoding temp = new UTF8Encoding(true);
			
			while (fs.Read(b,0,b.Length) > 0) 
			{
				sb.Append(temp.GetString(b));
			}
			fs.Close();
			
			fsData data;
			fsResult res = fsJsonParser.Parse(sb.ToString(), out data);
			if (res.Failed)
			{
				Debug.LogWarningFormat("Json file {0} parsed error {1}", field.Name, res.FormattedMessages);
				continue;
			}
			
			var value = field.GetValue(null);
			_serializer.TryDeserialize(data, field.FieldType, ref value).AssertSuccess();
			 field.SetValue(null, value);
		}
	}

	public static string configGeneratorMd5(Type type)
	{
		var gen = BinarySerializerCodeGenerator.GetGenerator(type);
		string serializerFileName = gen.SerializerFileName(type);
		Type t = Type.GetType(serializerFileName + ",Assembly-CSharp");
		if (t != null)
		{
			Type typesMd5Type = Type.GetType("TypesMd5,Assembly-CSharp");
			if (typesMd5Type == null)
				UnityEngine.Debug.LogError("Generate serializer code first");
			var fieldInfo = typesMd5Type.GetField("typeMd5");
			var typeMd5 = fieldInfo.GetValue(null) as Dictionary<Type,string>;
			string md5 = typeMd5[t];
			foreach (var dtype in gen.DirectlyUsedTypesExcludeSelf(type))
			{
				md5 += configGeneratorMd5(dtype);
			}
			return md5;
		}
		else 
			return "";
		
	}

	public static void ReadConfigAsBin(Type type, string name)
	{
		var gen = BinarySerializerCodeGenerator.GetGenerator(type);
		string serializerFileName = gen.SerializerFileName(type);
		if (!serializerFileName.StartsWith("Static_"))
		{
			UnityEngine.Debug.LogError("Config type must be static");
			return;
		}
		
		Type serializer = Type.GetType(serializerFileName + ",Assembly-CSharp");
		if (serializer != null)
		{
			MethodInfo read = serializer.GetMethod("Read", BindingFlags.Public | BindingFlags.Static);
			if (read != null)
			{
				string path = Path.Combine(ConfigExportPath.genBinPath, name + ".conf");
				BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open));
				byte[] md5 = br.ReadBytes(16);
				var same = Md5Utility.Md5Compare(md5, Md5Utility.MD5(configGeneratorMd5(type)));
				if (!same)
				{
					br.Close();
					UnityEngine.Debug.LogError("Read binary config error: md5 not the same");
					return;
				}
				read.Invoke(null, new object[]{br});
				br.Close();
			}
			else
				UnityEngine.Debug.LogError("Generate serializer code first");
		}
		else
			UnityEngine.Debug.LogError("Generate serializer code first");
	}
}
