using System;

public class PrimitiveGenerator : BaseGenerator {
	
	public override bool Accept(Type type)
	{
		return type.IsPrimitive;
	}
	
	public override string ReadExpression(Type type, string value)
	{
		if (type == typeof(Boolean))
			return string.Format("{0} = o.ReadBoolean()", value);
		else if (type == typeof(Byte))
			return string.Format("{0} = o.ReadByte()", value);
		else if (type == typeof(Char))
			return string.Format("{0} = o.ReadChar()", value);
		else if (type == typeof(Double))
			return string.Format("{0} = o.ReadDouble()", value);
		else if (type == typeof(Int16))
			return string.Format("{0} = o.ReadInt16()", value);
		else if (type == typeof(Int32))
			return string.Format("{0} = o.ReadInt32()", value);
		else if (type == typeof(Int64))
			return string.Format("{0} = o.ReadInt64()", value);
		else if (type == typeof(Single))
			return string.Format("{0} = o.ReadSingle()", value);
		else if (type == typeof(UInt16))
			return string.Format("{0} = o.ReadUInt16()", value);
		else if (type == typeof(UInt32))
			return string.Format("{0} = o.ReadUInt32()", value);
		else if (type == typeof(UInt64))
			return string.Format("{0} = o.ReadUInt64()", value);
		else
			throw new NotImplementedException("not implemented or bad primitive type: " + type.Name);
	}
	
	public override string WriteExpression(Type type, string value)
	{
		return string.Format("o.Write({0})", value);
	}
	
	public override Type[] TypeNameReferencedTypes(Type type)
	{
		return new []{type};
	}
	
	public override Type[] DirectlyUsedTypesExcludeSelf(Type type)
	{
		return new[] {typeof (Int32)};
	}
	
	public override string GenerateSerializerCode(Type type)
	{
		return null;
	}
}
