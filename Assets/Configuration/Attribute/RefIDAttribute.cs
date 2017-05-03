using System;
using System.Collections;
using System.Reflection;

[AttributeUsage(AttributeTargets.Field)]
public class RefIDAttribute : ValidateAttribute
{
	private FieldInfo dict;
	public RefIDAttribute(Type config, string dictname)
	{
		this.dict = config.GetField(dictname);
		if (!typeof(IDictionary).IsAssignableFrom(this.dict.FieldType))
		{
			throw new AttributeValidateException(config.Name, dictname, "RefIDAttribute: refer type must be dictionary");
		}
	}
	/// <summary>
	/// Validates the type. Only support enum, int, string for the key
	/// </summary>
	/// <param name="type">enum or int or string type</param>
	public override void ValidateType(Type type)
	{
		if (!type.IsEnum && !TypeUtility.IsIntegerType(type) && type != typeof(string))
			throw new AttributeValidateException(type.Name, "Reference ID only support enum, int, string type");

		var args = dict.FieldType.GetGenericArguments();
		var keyType = args[0];
		if (keyType != type)
		{
			throw new AttributeValidateException(type.Name, "Type not matches refer dict key type");
		}
	}

	public override void ValidateValue(System.Reflection.FieldInfo field, object data)
	{
		IDictionary dictionary = dict.GetValue(null) as IDictionary;
		if (!dictionary.Contains(data))
			throw new AttributeValidateException(field.Name, string.Format("Dict {0} not contains id: {1}", dict.Name, data));
	}
}
