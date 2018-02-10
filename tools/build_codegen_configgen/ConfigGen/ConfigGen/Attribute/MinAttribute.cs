using System;

namespace UF.Config.Attr
{
	[AttributeUsage(AttributeTargets.Field)]
	public class MinAttribute : ValidateAttribute
	{
		private double min;
		public MinAttribute(double min)
		{
			this.min = min;
		}
		public override void ValidateType(Type configType, Type type)
		{
			if (!TypeUtility.IsNumericType(type)) throw new AttributeValidateException(type.Name, "Min attribute should be on number type");
		}

		public override void ValidateValue(System.Reflection.FieldInfo field, object data, object configData)
		{
			double value = Convert.ToDouble(data);
			if (min - value > 0.0000001)
			{
				throw new AttributeValidateException(field.Name, string.Format("{0} Should not smaller than min value {1}", value, min));
			}
		}
	}
}
