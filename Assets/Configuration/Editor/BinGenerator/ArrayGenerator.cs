using System;
using System.Linq;

public class ArrayGenerator : BaseGenerator {

	public override bool Accept(Type type)
	{
		return type.IsArray;
	}

	public override string FileName(Type type)
	{
		Type valueType = type.GetElementType();
		var gen = BinarySerializerCodeGenerator.GetGenerator(valueType);
		return "Arr_" + gen.FileName(valueType);
	}

	public override string ReadExpression(Type type, string value)
	{
		return string.Format("{0} = {1}.Read(o)", value, SerializerFileName(type));
	}
	
	public override string WriteExpression(Type type, string value)
	{
		return string.Format("{1}.Write(o, {0})", value, SerializerFileName(type));
	}

	public override Type[] TypeNameReferencedTypes(Type type)
	{
		Type valueType = type.GetElementType();
		var gen = BinarySerializerCodeGenerator.GetGenerator(valueType);
		
		return new[] {type}
		.Union(gen.TypeNameReferencedTypes(valueType))
			.ToArray();
	}

	public override Type[] DirectlyUsedTypesExcludeSelf(Type type)
	{
		return new[] {type.GetElementType(), typeof(Int32)};
	}

	public override string GenerateSerializerCode(Type type)
	{
		var valueType = type.GetElementType();
		var gen = BinarySerializerCodeGenerator.GetGenerator(valueType);

		string read = GenArrayReadCode(valueType, gen);
		string write = GenArrayWriteCode(valueType, gen);
		return GenSerializerClassCodeByTemplate(type, TypeNameReferencedTypes(type), "null", read, write); 
	}

	private string GenArrayReadCode(Type valueType, BaseGenerator gen)
	{
		string valueTypeName = gen.TypeName(valueType);
		string str = "";
		if (valueTypeName.Contains("[]"))
		{
			valueTypeName = valueTypeName.Substring(0, valueTypeName.IndexOf('['));
			str = "[]";
		}
		return string.Format(
			"		int size = o.ReadInt32();\n" +
			"		d = new {0}[size]{2};\n" +
			"		for(int i = 0; i < size; ++i)\n" +
			"		{{\n" +
			"			{1};\n" +
			"		}}\n" +
			"",
			valueTypeName,
			gen.ReadExpression(valueType, "d[i]"),
			str
			);
	}
	
	private string GenArrayWriteCode(Type valueType, BaseGenerator gen)
	{
		return string.Format(
			"		int size = d.Length;\n"+
			"		o.Write(size);\n" +
			"		for(int i = 0; i < size; ++i)\n" +
			"		{{\n" +
			"			{0};\n" +
			"		}}\n" +
			"",
			gen.WriteExpression(valueType, "d[i]")
			);
	}
}
