using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEditor;

public class BinarySerializerCodeGenerator {

	private const string typeMd5CodeBegin = 
@"using System;
using System.Collections.Generic;
public static class TypesMd5
{ 
	public static Dictionary<Type, string> typeMd5 = new Dictionary<Type, string>();
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

	private readonly HashSet<Type> registeredTypes = new HashSet<Type>();

	public void Register(Type type)
	{
		if (type == null)
			return;
		if (registeredTypes.Contains(type))
			return;
		registeredTypes.Add(type);

		var gen = GetGenerator(type);
		Type[] directlyUsedTypes = gen.DirectlyUsedTypesExcludeSelf(type);
		foreach (var refType in directlyUsedTypes)
		{
			Register(refType);
		}
	}

	public void GenCode(string folder)
	{
		if (!Directory.Exists(folder))
		{
			Directory.CreateDirectory(folder);
		}

		//typeMd5 code
		string line = "\t\ttypeMd5[typeof({0})] = \"{1}\";\n";
		StringBuilder sb = new StringBuilder(typeMd5CodeBegin);
		foreach (var type in registeredTypes)
		{
			var gen = GetGenerator(type);
			string code = gen.GenerateSerializerCode(type);
			if (code != null)
			{
				string typename = gen.SerializerFileName(type);
				sb.Append(string.Format(line, typename, Md5Utility.MD5String(code)));
				File.WriteAllText(Path.Combine(folder, typename + ".cs"), code);
			}
		}
		sb.Append(typeMd5CodeEnd);
		File.WriteAllText(Path.Combine(folder, "TypesMd5.cs"), sb.ToString());
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
}


