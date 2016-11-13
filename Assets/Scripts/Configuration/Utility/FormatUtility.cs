using System.Text.RegularExpressions;
using UnityEngine.Assertions;

public static class FormatUtility
{
	public static string Format(string path, params object[] args)
	{
		return Parse(path, LaunchConfig.context, args);
	}

	public static string[] Format(string[] paths, params object[] args)
	{
		string[] results = new string[paths.Length];
		for (int i = 0; i < paths.Length; i++)
		{
			results[i] = Parse(paths[i], LaunchConfig.context, args);
		}
		return results;
	}

	/// <summary>
	///  用法：
	///     Replace( "{0} = ({x},{y}) ", new Point(99,88), "Point" )
	///  如果格式化时遇到不存在的键，则抛出异常
	/// </summary>
	public static string Parse(string str, object obj, params object[] args)
	{
		Assert.IsNotNull(obj);

		return Regex.Replace(str, @"{(\w+)}", o =>
			{
				string key = o.Groups[1].Value;
				int value = 0;
				if (int.TryParse(key, out value))
				{
					return args[value].ToString();
				}
				else
				{
					return obj.GetValueEx<string>(key);
				}
			});
	}
}
