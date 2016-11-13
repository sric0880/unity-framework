using System;

public class Base64Utils
{
	public static string Encode(byte[] inputBytes, int offset, int count)
	{
		return Convert.ToBase64String(inputBytes, offset, count);
	}

	public static byte[] Decode(string base64)
	{
		return Convert.FromBase64String(base64);
	}
}