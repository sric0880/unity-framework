using System;

public class DateTimeGenerator : BaseGenerator {

	public override bool Accept(Type type)
	{
		return type == typeof(DateTime);
	}

	public override string ReadExpression(Type type, string value)
	{
		return string.Format("{0} = new DateTime(o.ReadInt64())", value);
	}
	
	public override string WriteExpression(Type type, string value)
	{
		return string.Format("o.Write({0}.Ticks)", value);
	}
	
	public override Type[] TypeNameReferencedTypes(Type type)
	{
		return new []{type};
	}
	
	public override Type[] DirectlyUsedTypesExcludeSelf(Type type)
	{
		return new[] {typeof (Int64)};
	}
	
	public override string GenerateSerializerCode(Type type)
	{
		return null;
	}
}
