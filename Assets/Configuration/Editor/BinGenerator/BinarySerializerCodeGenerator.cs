using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class BinarySerializerCodeGenerator {

	private const string typeMd5CodeBegin = 
@"using System;
using System.Collections.Generic;
public static class TypesMd5
{ 
	public static Dictionary<string, string> typeMd5 = new Dictionary<string, string>();
	static TypesMd5()
	{
";
	private const string typeMd5CodeEnd = 
@"
	}
}";

	private static readonly BaseGenerator[] generators =
	{
		new PrimitiveGenerator(),
		new EnumGenerator(),
		new StringGenerator(),
		new ArrayGenerator(),
		new ListGenerator(), 
		new DictionaryGenerator(),
		new DateTimeGenerator(), 
		new ClassGenerator(),
		new StaticClassGenerator(),
	};

	private HashSet<Type> visitedCSTypes = new HashSet<Type>();
	private HashSet<Type> visitedLuaTypes = new HashSet<Type>();
	private readonly Dictionary<Type, byte[]> typeMd5 = new Dictionary<Type, byte[]>();
	private readonly Dictionary<string, string> finalTypeMd5 = new Dictionary<string, string>();
	private readonly HashSet<Type> registeredEnums = new HashSet<Type>();

	public void GenCode(Type mainType, string autoGenFolder)
	{
		Register(mainType, ExportAttributeType.CS, visitedCSTypes);
		Register(mainType, ExportAttributeType.LUA, visitedLuaTypes);
		if (!Directory.Exists(autoGenFolder))
		{
			Directory.CreateDirectory(autoGenFolder);
		}

		foreach (var type in visitedCSTypes)
		{
			if (typeMd5.ContainsKey(type))
			{
				continue;
			}
			var gen = GetGenerator(type);
			string code = gen.GenerateSerializerCode(type);
			if (code != null)
			{
				string typename = gen.SerializerFileName(type);
				typeMd5.Add(type, Md5Utility.MD5(code));
				File.WriteAllText(Path.Combine(autoGenFolder, typename + ".cs"), code);
			}
		}
		foreach (var type in visitedLuaTypes)
		{
			if (type.IsEnum)
			{
				registeredEnums.Add(type);
			}
		}
		visitedCSTypes.Clear();
		visitedLuaTypes.Clear();

		RebuildTypeMd5(mainType);
		foreach (var type in visitedCSTypes)
		{
			var gen = GetGenerator(type);
			string typename = gen.SerializerFileName(type);
			if (finalTypeMd5.ContainsKey(typename))
			{
				continue;
			}
			finalTypeMd5.Add(typename, Md5Utility.Md5ToString(typeMd5[type]));
		}
	}

	public void WriteTypeMd5File(string folder)
	{
		string line = "\t\ttypeMd5[\"{0}\"] = \"{1}\";\n";
		StringBuilder sb = new StringBuilder(typeMd5CodeBegin);
		foreach (var pair in finalTypeMd5)
		{
			sb.Append(string.Format(line, pair.Key, pair.Value));
		}
		sb.Append(typeMd5CodeEnd);
		File.WriteAllText(Path.Combine(folder, "TypesMd5.cs"), sb.ToString());
	}

	public void WriteLuaEnumsFile(string luaExportDir)
	{
		StringBuilder builder = new StringBuilder();
		foreach (var e in registeredEnums)
		{
			builder.Append(e.Name + "={\n");
			foreach (var name in Enum.GetNames(e))
			{
				int value = (int)Enum.Parse(e, name);
				builder.Append(string.Format("\t{0} = {1},\n", name, value));
			}
			builder.Append("}\n");
		}
		File.WriteAllText(Path.Combine(luaExportDir, "global_enums.lua"), builder.ToString());
	}

	public static BaseGenerator GetGenerator(Type type)
	{
		foreach (var gen in generators)
		{
			if (gen.Accept(type))
				return gen;
		}
		return null;
	}

	private void Register(Type type, ExportAttributeType exportType, HashSet<Type> visitedTypes)
	{
		if (type == null)
			return;
		if (visitedTypes.Contains(type))
			return;
		visitedTypes.Add(type);
		var gen = GetGenerator(type);
		if (gen is StaticClassGenerator)
		{
			var fields = ClassFieldFilter.GetConfigFieldInfo(type, exportType);
			foreach (var field in fields)
			{
				Register(field.FieldType, exportType, visitedTypes);
			}
		}
		else
		{
			Type[] directlyUsedTypes = gen.DirectlyUsedTypesExcludeSelf(type);
			foreach (var refType in directlyUsedTypes)
			{
				Register(refType, exportType, visitedTypes);
			}
		}
	}

	private bool RebuildTypeMd5(Type mainType)
	{
		byte[] md5;
		if (!typeMd5.TryGetValue(mainType, out md5))
		{
			return false;
		}
		if (visitedCSTypes.Contains(mainType))
		{
			return true;
		}
		visitedCSTypes.Add(mainType);
		var gen = GetGenerator(mainType);
		var refTypes = gen.DirectlyUsedTypesExcludeSelf(mainType);
		if (refTypes.Length != 0)
		{
			List<byte> bytes = new List<byte>(md5);
			foreach (var dtype in refTypes)
			{
				if (RebuildTypeMd5(dtype))
				{
					bytes.AddRange(typeMd5[dtype]);
				}
			}
			typeMd5[mainType] = Md5Utility.MD5(bytes.ToArray());
		}
		return true;
	}
}