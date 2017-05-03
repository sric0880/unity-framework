using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public abstract class BaseGenerator{

	public const int InitStringBuilderCapacity = 1024;

	public abstract bool Accept(Type type);
	//for generating file name
	public virtual string FileName(Type type)
	{
		return type.Name;
	}
	//using in serializer code
	public virtual string TypeName(Type type)
	{
		return type.Name;
	}
	public abstract string ReadExpression(Type type, string value);
	public abstract string WriteExpression(Type type, string value);
	//for writing namespace
	public abstract Type[] TypeNameReferencedTypes(Type type);
	//for writing serializer code
	public abstract Type[] DirectlyUsedTypesExcludeSelf(Type type);
	public abstract string GenerateSerializerCode(Type type);

	public string SerializerFileName(Type type)
	{
		return TypeUtility.GetSerializeTypeName(FileName(type));
	}

	public static void GenHeaderComment(StringBuilder code)
	{
		code.Append("// Auto generated code\n");
	}

	protected string DefaultValue(Type type)
	{
		if (type.IsValueType)
			return string.Format("new {0}()", TypeName(type));
		else
			return "null";
	}
	
	protected string GenSerializerClassCodeByTemplate(Type type, Type[] refTypes, string construction, string contentRead, string contentWrite)
	{
		StringBuilder code = new StringBuilder(InitStringBuilderCapacity);
		GenHeaderComment(code);
		NamespaceUtility.GenUsingDirectives(code, refTypes, new[] { "System.IO" });
		
		code.Append(
			string.Format(
			"\n" +
			"public class {0}\n" +
			"{{\n" +
			"	public static {1} Read(BinaryReader o)\n" +
			"	{{\n" +
			(
			type.IsClass ?
			"		if(o.ReadBoolean() == false)\n" +
			"			return {4};\n" :""
			)+
			"		\n" +
			"		{1} d = {5};\n" +
			"{2}" +
			"		return d;\n" +
			"	}}\n" +
			"\n" +
			"	public static void Write(BinaryWriter o, {1} d)\n" +
			"	{{\n" +
			(
			type.IsClass ?
			"		o.Write(d != null);\n" +
			"		if(d == null)\n" +
			"			return;\n":""
			)+
			"		\n" + 
			"{3}" +
			"	}}\n" +
			"}}\n",
			SerializerFileName(type),
			TypeName(type),
			contentRead,
			contentWrite,
			DefaultValue(type),
			construction
			));
		
		return code.ToString();
	}
	
	protected string GenSerializerStaticClassCodeByTemplate(Type type, Type[] refTypes, string contentRead, string contentWrite)
	{
		StringBuilder code = new StringBuilder(InitStringBuilderCapacity);
		GenHeaderComment(code);
		NamespaceUtility.GenUsingDirectives(code, refTypes, new []{"System.IO"});
		
		code.Append(
			string.Format(
			"\n" +
			"public class {0}\n" +
			"{{\n" +
			"	public static void Read(BinaryReader o)\n" +
			"	{{\n" +
			"{1}" +
			"	}}\n" +
			"\n" +
			"	public static void Write(BinaryWriter o)\n" +
			"	{{\n" +
			"{2}" +
			"	}}\n" +
			"}}\n",
			SerializerFileName(type),
			contentRead,
			contentWrite
			));
		
		return code.ToString();
	}
}
