using System;
public class AttributeValidateException : Exception
{
	public string classname;
	public string fieldname;
	public string errmsg;

	public AttributeValidateException(string errmsg) : this("Unknown", "Unknown", errmsg)
	{
	}

	public AttributeValidateException(string fieldname, string errmsg) : this("Unknown", fieldname, errmsg)
	{
	}

	public AttributeValidateException(string classname, string fieldname, string errmsg)
	{
		this.classname = classname;
		this.fieldname = fieldname;
		this.errmsg = errmsg;
	}

	public override string Message
	{
		get
		{
			return string.Format("Class:{0}, Field:{1}, Error:{2}", this.classname, this.fieldname, this.errmsg);
		}
	}
}

public abstract class ValidateAttribute : Attribute
{
	public abstract void ValidateType(Type type);
	public abstract void ValidateValue(System.Reflection.FieldInfo field, object data);
}
