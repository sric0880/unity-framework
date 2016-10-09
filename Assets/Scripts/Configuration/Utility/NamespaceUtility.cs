using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

public class NamespaceUtility {

	// 由于mono下 nested class 的 .NameSpace属性有问题，所以整了这个 workaround
	public static string GetNameSpace(Type o)
	{
		if (o == null) UnityEngine.Debug.Log("owwwwwwwwwwwww");
		var nameSpace = o.Namespace;
		if (!string.IsNullOrEmpty(nameSpace))
			return nameSpace;
		
		while (o.HasElementType)
		{
			o = o.GetElementType();
		}
		
		while (o.IsNested)
		{
			o = o.DeclaringType;
		}
		return o.Namespace;
	}

	public static string[] GetNameSpacesWithGeneric(Type o)
	{
		var ns = new List<string>();
		string name = GetNameSpace(o);
		if (!string.IsNullOrEmpty(name)) ns.Add(name);
		
		foreach (var type in o.GetGenericArguments())
		{
			name = GetNameSpace(type);
			if (!string.IsNullOrEmpty(name) && !ns.Contains(name)) ns.Add(name);
		}
		return ns.ToArray();
	}
	
	public static void GenUsingDirectives(StringBuilder code, Type[] refTypes, string[] extraUsing)
	{
		var namespaces = refTypes.SelectMany(o => GetNameSpacesWithGeneric(o))
			.Concat(extraUsing)
				.Where(o => !string.IsNullOrEmpty(o))
				.Distinct()
				.OrderBy(o => o);
		
		foreach (var ns in namespaces)
		{
			code.Append(string.Format("using {0};\n", ns));
		}
	}

}
