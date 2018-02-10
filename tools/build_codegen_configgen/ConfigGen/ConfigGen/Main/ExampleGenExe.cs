using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using FullSerializer;
using UF.Config.Attr;

namespace UF.Config
{
	public static class ExampleGenExe {

		// for writing json file
		private static Dictionary<string, LocaleJsonObject> localeDict = new Dictionary<string, LocaleJsonObject>();
		private static readonly fsSerializer _serializer = new fsSerializer();

		public static void Main(string[] args)
		{
			SaveConfigAsJson(args[0]);
		}

		private static void SaveConfigAsJson(string exampleConfigPath)
		{
			localeDict.Add("LOCALE_ID", new LocaleJsonObject());

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
						string folder = Path.Combine(exampleConfigPath, attr.dirName);
						if (!Directory.Exists(folder))
						{
							Directory.CreateDirectory(folder);
						}
						TypeUtility.ValidateType(type, type);
						WriteConfigAsJson(type, folder);
					}
				}
			}
		}

		private static void WriteConfigAsJson(Type type, string folder)
		{
			var exportFields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
			if (exportFields.Length == 0)
			{
				return;
			}
			_serializer.Config.CustomDateTimeFormatString = "G";
			_serializer.AddConverter(new MyCustomEnumConverter());
			_serializer.AddConverter(new MyDictionaryConverter());
			var ins = InstanceUtility.InstanceOfType(type);
			foreach (var field in exportFields)
			{
				if (field.GetValue(ins) == null)
				{
					var value = InstanceUtility.InstanceOfType(field.FieldType);
					field.SetValue(ins, value);
				}
				XlsxNameAttribute xlxsName = (XlsxNameAttribute)Attribute.GetCustomAttribute(field, typeof(XlsxNameAttribute));
				string jsonFilename = xlxsName != null ? xlxsName.xlsxName : field.Name;
				var file = Path.Combine(folder, jsonFilename + ".json");
				fsData data;
				_serializer.TrySerialize(field.FieldType, field.GetValue(ins), out data).AssertSuccess();
				WriteDataToJson(file, data);
				if (TypeUtility.HasAttribute<LocaleAttribute>(field.FieldType))
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
	}
}