using System.Text.RegularExpressions;
using UnityEngine.Assertions;

/// <summary>
///  用法：
///     R( "{0} = ({x},{y}) ", new Point(99,88), "Point" )
///  如果格式化时遇到不存在的键，则抛出异常
/// </summary>
public class Replace
{
	public static string R(string format, object obj, params object[] args)
	{
		Assert.IsNotNull(obj);

		return Regex.Replace(format, @"{(\w+)}", o =>
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
