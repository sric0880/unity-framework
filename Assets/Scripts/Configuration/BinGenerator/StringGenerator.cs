using System;

public class StringGenerator : BaseGenerator {

	public override bool Accept(Type type)
	{
		return type == typeof(string);
	}

	public override string TypeName(Type type)
	{
		return "string";
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
		return new Type[]{};	
	}
	
	public override string GenerateSerializerCode(Type type)
	{
		return GenSerializerClassCodeByTemplate(type, TypeNameReferencedTypes(type), 
		                                        string.Format("null"),
		                                        "			d = o.ReadString();\n",
		                                        "			o.Write(d);\n"
		                                        );
	}
}
