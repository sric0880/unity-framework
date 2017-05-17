using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Field)]
public class IDAttribute : Attribute
{
	public static string TypeHasIDAttr(Type type)
	{
		foreach (var field in type.GetFields())
		{
			if (field.IsDefined(typeof(IDAttribute), true))
			{
				return field.Name;
			}
		}
		return null;
	}
}
