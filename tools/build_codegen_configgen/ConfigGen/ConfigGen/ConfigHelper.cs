using System;
using System.IO;
using UF.Config.Attr;

namespace UF.Config
{
	public static class ConfigHelper
	{
		public static string GetExportDirName<T>()
		{
			var attrs = typeof(T).GetCustomAttributes(typeof(ExportAttribute), false);
			if (attrs.Length > 0) {
				ExportAttribute attr = (ExportAttribute)attrs[0];
				return attr.dirName;
			}
			return null;
		}

		public static T ReadConfigAsBin<T>(Stream stream) where T : ISerializable, new(){
			if (stream != null) {
				T ret = new T();
				BinaryReader br = new BinaryReader (stream);
				ret.Deserialize(br);
				br.Close ();
				return ret;
			}
			return default(T);
		}
	}
}