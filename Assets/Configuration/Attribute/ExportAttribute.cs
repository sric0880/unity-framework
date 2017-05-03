using System;
using System.Collections;

public class ExportAttribute : ValidateAttribute
{
	private string name;
	private string key;

	public ExportAttribute() { }
	public ExportAttribute(string name)
	{
		this.name = name;
	}
	public ExportAttribute(string name, string key)
	{
		this.name = name;
		this.key = key;
	}

	public string Name
	{
		get
		{
			return this.name;
		}
	}

	public override void ValidateType(Type type)
	{
		if (string.IsNullOrEmpty(key)) return;
		//1. FieldInfo must be dictionary
		if (!typeof(IDictionary).IsAssignableFrom(type))
		{
			throw new AttributeValidateException("Not dictionary type");
		}
		//2. ValueType must contains field named key
		var args = type.GetGenericArguments();
		var keyType = args[0];
		var valueType = args[1];
		if (!valueType.IsClass)
		{
			throw new AttributeValidateException("Value type is not type of class");
		}
		foreach (var field in ClassFieldFilter.GetClassFieldInfo(valueType))
		{
			if (field.Name.Equals(key))
			{
				if (keyType != field.FieldType)
					throw new AttributeValidateException(string.Format("Key type {0} is not matched with type of {1}", keyType.Name, key));
				else
					return;
			}
		}
		throw new AttributeValidateException(string.Format("Value type hasn't field {0}", key));
	}

	public override void ValidateValue(System.Reflection.FieldInfo field, object data)
	{
	}
}
