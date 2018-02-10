using System;

namespace UF.Config.Attr
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ExportAttribute : Attribute
	{
		public string dirName;

		public ExportAttribute(string dirName) {
			this.dirName = dirName;
		}
	}
}
