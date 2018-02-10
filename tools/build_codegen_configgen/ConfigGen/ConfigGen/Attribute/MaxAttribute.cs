using System;

namespace UF.Config.Attr
{
	[AttributeUsage(AttributeTargets.Field)]
	public class MaxAttribute : ValidateAttribute
	{
		private double max;
		public MaxAttribute(double max)
		{
			this.max = max;
		}
		public override void ValidateType(Type configType, Type type)
		{
			if (!TypeUtility.IsNumericType(type)) throw new AttributeValidateException(type.Name, "Max attribute should be on number type");
		} 

		public override void ValidateValue(System.Reflection.FieldInfo field, object data, object configData)
		{
			double value = Convert.ToDouble(data);
			if (value - max > 0.0000001)
			{
				throw new AttributeValidateException(field.Name, string.Format("{0} Should not larger than max value {1}", value, max));
			}
		}
	}
}
