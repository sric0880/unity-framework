using UnityEditor;
using System;
using System.Threading;
using System.Globalization;

public class DateTimeInspector : DataInspector {

	public override bool canFoldout()
	{
		return false;
	}
	
	public override bool inspect(ref object data, Type type, string name, string path)
	{
		Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
		DateTime temp;
		if (DateTime.TryParse(EditorGUILayout.TextField(name, data.ToString()), out temp))
		{
			return applyData(ref data, temp);
		}
		return false;
	}
}
