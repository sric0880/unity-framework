using System;
using System.Collections;

public class TypeUtility {

	public static string GetGenericTypeName(Type type)
	{
		if (!type.IsGenericType)
			throw new ArgumentException("type is not generic");
		return type.Name.Substring(0, type.Name.IndexOf("`"));
	}
}
