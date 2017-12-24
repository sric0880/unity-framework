using System;
using System.IO;
using System.Reflection;

public static class ConfigReader {
	public static void ReadConfigAsBin (Type type, string name) {
		string serializerFileName = TypeUtility.GetSerializeTypeName (TypeUtility.GetStaticTypeName (type));
		if (!serializerFileName.StartsWith ("Static_")) {
			Log.Error ("Config type must be static");
			return;
		}

		Type serializer = Type.GetType (serializerFileName + ",Assembly-CSharp");
		if (serializer != null) {
			MethodInfo read = serializer.GetMethod ("Read", BindingFlags.Public | BindingFlags.Static);
			if (read != null) {
				string path = Path.Combine (FileUtils.binary_config_folder, name + ".bin");
				var stream = FileUtils.GetMemoryStreamFromFile (path);
				if (stream != null) {
					BinaryReader br = new BinaryReader (stream);
					string md5 = br.ReadString ();
					if (md5 != TypesMd5.typeMd5[serializerFileName]) {
						br.Close ();
						throw new Exception ("Read binary config error: md5 not the same");
					}
					read.Invoke (null, new object[] { br });
					br.Close ();
				}
			} else
				throw new Exception ("Generate serializer code first");
		} else
			throw new Exception ("Generate serializer code first");
	}

	public static void ReadConfigAsBinAsync (Type type, string name) {
		string serializerFileName = TypeUtility.GetSerializeTypeName (TypeUtility.GetStaticTypeName (type));
		if (!serializerFileName.StartsWith ("Static_")) {
			Log.Error ("Config type must be static");
			return;
		}

		Type serializer = Type.GetType (serializerFileName + ",Assembly-CSharp");
		if (serializer != null) {
			MethodInfo read = serializer.GetMethod ("Read", BindingFlags.Public | BindingFlags.Static);
			if (read != null) {
				string path = Path.Combine (FileUtils.binary_config_folder, name + ".bin");
				FileUtils.GetMemoryStreamFromFileAsync (path, (stream) => {
					if (stream != null) {
						BinaryReader br = new BinaryReader (stream);
						string md5 = br.ReadString ();
						if (md5 != TypesMd5.typeMd5[serializerFileName]) {
							br.Close ();
							throw new Exception ("Read binary config error: md5 not the same");
						}
						read.Invoke (null, new object[] { br });
						br.Close ();
					}
				});
			} else
				throw new Exception ("Generate serializer code first");
		} else
			throw new Exception ("Generate serializer code first");
	}
}