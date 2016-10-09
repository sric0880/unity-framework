using System;
using System.Linq;
using System.Collections.Generic;

public class ListGenerator : BaseGenerator {

	public override bool Accept(Type type)
	{
		if (type.IsGenericType)
		{
			var typeDef = type.GetGenericTypeDefinition();
			if (typeDef == typeof(List<>))
				return true;
		}
		return false;
	}

	public override string FileName(Type type)
	{
		var valueType = type.GetGenericArguments()[0];
		var gen = BinarySerializerCodeGenerator.GetGenerator(valueType);
		return TypeUtility.GetGenericTypeName(type) + "_" + gen.FileName(valueType);
	}
	
	public override string TypeName(Type type)
	{
		var valueType = type.GetGenericArguments()[0];
		var gen = BinarySerializerCodeGenerator.GetGenerator(valueType);
		return string.Format("{0}<{1}>", TypeUtility.GetGenericTypeName(type), gen.TypeName(valueType));
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
		var valueType = type.GetGenericArguments()[0];
		var gen = BinarySerializerCodeGenerator.GetGenerator(valueType);

		return new[] {type}
		.Union(gen.TypeNameReferencedTypes(valueType))
			.ToArray();
	}
	
	public override Type[] DirectlyUsedTypesExcludeSelf(Type type)
	{
		var valueType = type.GetGenericArguments()[0];
		return new[] { valueType, typeof(Int32) };
	}
	
	public override string GenerateSerializerCode(Type type)
	{
		var valueType = type.GetGenericArguments()[0];
		var gen = BinarySerializerCodeGenerator.GetGenerator(valueType);

		string read = GenListReadCode(type, valueType, gen);
		string write = GenListWriteCode(type, valueType, gen);
		
		return GenSerializerClassCodeByTemplate(type, TypeNameReferencedTypes(type), "null", read, write);
	}

	private string GenListReadCode(Type type, Type valueType, BaseGenerator gen)
	{
		return string.Format(
			"		int size = o.ReadInt32();\n" +
			"		d = new {0}(size);\n" +
			"		for(int i = 0; i < size; ++i)\n" +
			"		{{\n" +
			"			{1} elem;\n"+
			"			{2};\n" +
			"			d.Add(elem);\n" +
			"		}}\n" +
			"",
			TypeName(type), 
			gen.TypeName(valueType),
			gen.ReadExpression(valueType, "elem")
			);
	}
	
	private string GenListWriteCode(Type type, Type valueType, BaseGenerator gen)
	{
		return string.Format(
			"		int size = d.Count;\n"+
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
