using System;

public class EnumGenerator : BaseGenerator {

	public override bool Accept(Type type)
	{
		return type.IsEnum;
	}
	
	public override string ReadExpression(Type type, string value)
	{
		return string.Format("{0} = ({1})o.ReadInt32()", value, type.Name);
	}
	
	public override string WriteExpression(Type type, string value)
	{
		return string.Format("o.Write((int){0})", value);
	}
	
	public override Type[] TypeNameReferencedTypes(Type type)
	{
		return new []{type};
	}
	
	public override Type[] DirectlyUsedTypesExcludeSelf(Type type)
	{
		return new Type[] {};
	}
	
	public override string GenerateSerializerCode(Type type)
	{
		return null;
	}

}
