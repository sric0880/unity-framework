using System;
using System.Text.RegularExpressions;

namespace UF.Config.Attr
{
	[AttributeUsage(AttributeTargets.Field)]
	public class LocaleAttribute : ValidateAttribute
	{
		public override void ValidateType(Type configType, Type type)
		{
			if (type != typeof(string)) throw new AttributeValidateException(type.Name, "Locale attribute is not on string type");
		}

		public override void ValidateValue(System.Reflection.FieldInfo field, object data, object configData)
		{
			Regex reg = new Regex("^[A-Za-z0-9_]*$");
			if (!reg.IsMatch(data as string))
			{
				throw new AttributeValidateException(field.Name, "Locale string consists only letter, number or underscore");
			}
		}
	}
}