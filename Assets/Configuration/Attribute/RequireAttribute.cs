using System;
using System.Collections;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Field)]
public class RequireAttribute : ValidateAttribute
{
	public override void ValidateType(Type type)
	{
	}

	public override void ValidateValue(System.Reflection.FieldInfo field, object data)
	{
		var type = field.FieldType;
		if (TypeUtility.IsNumericType(type))
		{
			if (Math.Abs(Convert.ToDouble(data)) < double.Epsilon) throw new AttributeValidateException(field.Name, "value could not be zero");
		}
		else if (type == typeof(string))
		{
			if (string.IsNullOrEmpty((data as string))) throw new AttributeValidateException(field.Name, "value could not be null or empty");
		}
		else if (type.IsArray)
		{
			if ((data as Array).Length == 0) throw new AttributeValidateException(field.Name, "array must not be empty");
		}
		else if (type == typeof(DateTime))
		{
			//TODO: 
		}
		else if (type.IsGenericType)
		{
			var typeDef = type.GetGenericTypeDefinition();
			if (typeDef == typeof(List<>))
			{
				if ((data as IList).Count == 0) throw new AttributeValidateException(field.Name, "list must not be empty");
			}
			if (typeDef == typeof(Dictionary<,>))
			{
				if ((data as IDictionary).Count == 0) throw new AttributeValidateException(field.Name, "dictionary must not be empty");
			}
		}
	}
}
