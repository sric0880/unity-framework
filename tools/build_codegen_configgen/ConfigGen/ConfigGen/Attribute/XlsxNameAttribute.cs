using System;

namespace UF.Config.Attr
{
	[AttributeUsage(AttributeTargets.Field)]
	public class XlsxNameAttribute : Attribute
	{
		public string xlsxName;

		public XlsxNameAttribute(string xlsxName) {
			this.xlsxName = xlsxName;
		}
	}
}