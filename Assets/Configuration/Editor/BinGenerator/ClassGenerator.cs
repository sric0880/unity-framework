using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

public class ClassGenerator : BaseGenerator {

	public override bool Accept(Type type)
	{
		return (type.IsClass && !type.IsAbstract) || 
			(type.IsValueType && !type.IsEnum && !type.IsPrimitive && !type.IsAbstract);
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
		return new []{type};
	}
	
	public override Type[] DirectlyUsedTypesExcludeSelf(Type type)
	{
		var fields = ClassFieldFilter.GetClassFieldInfo (type);
		return fields.Select(f => f.FieldType).ToArray();	
	}

	public override string GenerateSerializerCode(Type type)
	{
		var fields = ClassFieldFilter.GetClassFieldInfo (type);
		return GenSerializerClassCodeByTemplate(
			type, 
			FieldReferencedTypes(fields).Union(TypeNameReferencedTypes(type)).ToArray(),
			string.Format("new {0}()", type.Name),
			GenFieldReadCode(fields), 
			GenFieldWriteCode(fields));	
	}

	private Type[] FieldReferencedTypes(List<FieldInfo> fields)
	{
		if (fields.Count == 0)
		return new Type[]{};
		
		Type[] types = new Type[fields.Count];
		int i = 0;
		foreach (var field in fields)
		{
			types[i++] = field.FieldType;
		}
		return types;
	}

	private string GenFieldReadCode(List<FieldInfo> fields)
	{
		StringBuilder code = new StringBuilder(InitStringBuilderCapacity);
		foreach (var field in fields)
		{
			string value = "d." + field.Name;
			var gen = BinarySerializerCodeGenerator.GetGenerator(field.FieldType);
			code.Append(string.Format("			{0};\n", gen.ReadExpression(field.FieldType, value)));
		}
		return code.ToString();
	}
	
	private string GenFieldWriteCode(List<FieldInfo> fields)
	{
		StringBuilder code = new StringBuilder(InitStringBuilderCapacity);
		foreach (var field in fields)
		{
			string value = "d." + field.Name;
			var gen = BinarySerializerCodeGenerator.GetGenerator(field.FieldType);
			code.Append(string.Format("			{0};\n", gen.WriteExpression(field.FieldType, value)));
		}
		return code.ToString();
	}
}
