using System;
using System.Collections;
using System.Reflection;

namespace UF.Config.Attr
{
	[AttributeUsage(AttributeTargets.Field)]
	public class RefIDAttribute : ValidateAttribute
	{
		private string dictname;

		public RefIDAttribute(string dictname)
		{
			this.dictname = dictname;
		}

		/// <summary>
		/// Validates the type. Only support enum, int, string for the key
		/// </summary>
		/// <param name="type">enum or int or string type</param>
		public override void ValidateType(Type configType, Type type)
		{
			FieldInfo dict = configType.GetField(dictname);
			if (dict == null)
			{
				throw new AttributeValidateException(configType.Name, dictname, "refered dict not exists in config class");
			}
			if (!typeof(IDictionary).IsAssignableFrom(dict.FieldType))
			{
				throw new AttributeValidateException(configType.Name, dictname, "refered type must be dictionary");
			}	

			if (!type.IsEnum && !TypeUtility.IsIntegerType(type) && type != typeof(string))
				throw new AttributeValidateException(type.Name, "Reference ID only support enum, int, string type");

			var args = dict.FieldType.GetGenericArguments();
			var keyType = args[0];
			if (keyType != type)
			{
				throw new AttributeValidateException(type.Name, "Type not matches refer dict key type");
			}
		}

		public override void ValidateValue(System.Reflection.FieldInfo field, object data, object configData)
		{
			Type configType = configData.GetType();
			FieldInfo dict = configType.GetField(dictname);
			IDictionary dictionary = dict.GetValue(configData) as IDictionary;
			if (!dictionary.Contains(data))
				throw new AttributeValidateException(field.Name, string.Format("Dict {0} not contains id: {1}", dict.Name, data));
		}
	}
}
