using System;
using System.Reflection;

// 加上该属性时，导出Excel中，如果是字典类型，那么将标记的字段
// 作为Key，而不使用多余的字段作为Key
// 和DictAttribute配套使用

namespace UF.Config.Attr
{
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
}
