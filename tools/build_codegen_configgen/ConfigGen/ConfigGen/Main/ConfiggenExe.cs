using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using FullSerializer;
using UF.Config.Attr;

namespace UF.Config
{
	public static class ConfiggenExe
	{
		private static readonly fsSerializer _serializer = new fsSerializer();
		private static Dictionary<string, Type> exportTypes = new Dictionary<string, Type>();

		public static void Main(string[] args)
		{
			Assembly[] assemblies =  AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				string assemblyName = assembly.GetName().Name;
				if (assemblyName == "mscorlib" || assemblyName.StartsWith("System"))
				{
					continue;
				}
				foreach(Type type in assembly.GetTypes()) {
					var attrs = type.GetCustomAttributes(typeof(ExportAttribute), false);
					if (attrs.Length > 0) {
						ExportAttribute attr = (ExportAttribute)attrs[0];
						exportTypes.Add(attr.dirName, type);
					}
				}
			}

			string exportedJsonFolder = args[0];
			string exportBinFolder = args[1];
			ReadConfigAsJsonThenSaveAsBin(exportedJsonFolder, exportBinFolder);
		}

		private static void ReadConfigAsJsonThenSaveAsBin(string exportedJsonFolder, string exportBinFolder)
		{
			foreach (var entry in exportTypes)
			{
				string folder = Path.Combine(exportedJsonFolder, entry.Key);
				var ins = ReadConfigAsJson(entry.Value, folder);
				TypeUtility.ValidateValue(ins, ins, entry.Value);
				WriteConfigAsBin(entry.Value, ins, entry.Key, exportBinFolder);
			}
		}

		private static object ReadConfigAsJson(Type type, string folder)
		{
			var ins = Activator.CreateInstance(type);
			var exportFields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
			if (exportFields.Length == 0)
			{
				return ins;
			}
			_serializer.AddConverter(new MyCustomEnumConverter());
			_serializer.AddConverter(new MyDictionaryConverter());
			foreach (var field in exportFields)
			{
				XlsxNameAttribute xlxsName = (XlsxNameAttribute)Attribute.GetCustomAttribute(field, typeof(XlsxNameAttribute));
				string jsonFilename = xlxsName != null ? xlxsName.xlsxName : field.Name;
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
				object value = null;
				_serializer.TryDeserialize(data, field.FieldType, ref value).AssertSuccess();
				field.SetValue(ins, value);
			}
			return ins;
		}

		private static void WriteConfigAsBin(Type type, object value, string name, string folder)
		{
			MethodInfo write = type.GetMethod("Serialize", BindingFlags.Public | BindingFlags.Instance);
			if (write != null)
			{
				string path = Path.Combine(folder, name + ".bin");
				BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create));
				write.Invoke(value, new object[] { bw });
				bw.Close();
			}
			else
			{
				throw new Exception("Generate serializer code first");
			}
		}
	}
}