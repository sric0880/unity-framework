using System;
using System.Linq;
using System.Collections.Generic;

public class DictionaryGenerator : BaseGenerator {

	public override bool Accept(Type type)
	{
		if (type.IsGenericType)
		{
			var typeDef = type.GetGenericTypeDefinition();
			if (typeDef == typeof(Dictionary<,>))
				return true;
		}
		return false;
	}

	public override string FileName(Type type)
	{
		var args = type.GetGenericArguments();
		var keyType = args[0];
		var valueType = args[1];
		var keyGen = BinarySerializerCodeGenerator.GetGenerator(keyType);
		var valueGen = BinarySerializerCodeGenerator.GetGenerator(valueType);
		
		return string.Format("{0}_{1}_{2}", TypeUtility.GetGenericTypeName(type), keyGen.FileName(keyType), valueGen.FileName(valueType));
	}
	
	public override string TypeName(Type type)
	{
		var args = type.GetGenericArguments();
		var keyType = args[0];
		var valueType = args[1];
		var keyGen = BinarySerializerCodeGenerator.GetGenerator(keyType);
		var valueGen = BinarySerializerCodeGenerator.GetGenerator(valueType);
		return string.Format("{0}<{1}, {2}>", TypeUtility.GetGenericTypeName(type), keyGen.TypeName(keyType), valueGen.TypeName(valueType));	
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
		var args = type.GetGenericArguments();
		var keyType = args[0];
		var valueType = args[1];
		var keyGen = BinarySerializerCodeGenerator.GetGenerator(keyType);
		var valueGen = BinarySerializerCodeGenerator.GetGenerator(valueType);

		return new[] {type}
		.Union(keyGen.TypeNameReferencedTypes(keyType))
			.Union(valueGen.TypeNameReferencedTypes(valueType))
				.ToArray();
	}
	
	public override Type[] DirectlyUsedTypesExcludeSelf(Type type)
	{
		var args = type.GetGenericArguments();
		var keyType = args[0];
		var valueType = args[1];
		return new[] {keyType, valueType, typeof(Int32)};
	}
	
	public override string GenerateSerializerCode(Type type)
	{
		var args = type.GetGenericArguments();
		var keyType = args[0];
		var valueType = args[1];
		var keyGen = BinarySerializerCodeGenerator.GetGenerator(keyType);
		var valueGen = BinarySerializerCodeGenerator.GetGenerator(valueType);

		string read = GenListReadCode(type, keyType, valueType, keyGen, valueGen);
		string write = GenListWriteCode(type, keyType, valueType, keyGen, valueGen);
		
		return GenSerializerClassCodeByTemplate(type, TypeNameReferencedTypes(type), "null", read, write);
	}

	protected virtual string GenListReadCode(Type type, Type keyType, Type valueType, BaseGenerator keyGen, BaseGenerator valueGen)
	{
		return string.Format(
			"		int size = o.ReadInt32();\n" +
			"		d = new {0}(size);\n" +
			"		for(int i = 0; i < size; ++i)\n" +
			"		{{\n" +
			"			{1} key;\n" +
			"			{2};\n" +
			"			{3} value;\n"+
			"			{4};\n" +
			"			d.Add(key, value);\n" +
			"		}}\n",
			TypeName(type),
			keyGen.TypeName(keyType),
			keyGen.ReadExpression(keyType, "key"),
			valueGen.TypeName(valueType),
			valueGen.ReadExpression(valueType, "value")
			);
	}
	
	protected virtual string GenListWriteCode(Type type, Type keyType, Type valueType, BaseGenerator keyGen, BaseGenerator valueGen)
	{
		return string.Format(
			"		int size = d.Count;\n"+
			"		o.Write(size);\n" +
			"		foreach(var elem in d)\n" +
			"		{{\n" +
			"			{0};\n" +
			"			{1};\n" +
			"		}}\n" +
			"",
			keyGen.WriteExpression(keyType, "elem.Key"),
			valueGen.WriteExpression(valueType, "elem.Value")
			);
	}
}
