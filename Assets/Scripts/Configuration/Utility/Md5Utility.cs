using System.Security.Cryptography;
using System.Text;

public class Md5Utility{

	private static readonly MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
	private static readonly StringBuilder str = new StringBuilder(16);
	
	public static byte[] MD5(byte[] data, int offset, int count)
	{
		return md5.ComputeHash(data, offset, count);
	}
	
	public static byte[] MD5(byte[] data)
	{
		return md5.ComputeHash(data);
	}
	
	public static byte[] MD5(string data)
	{
		return md5.ComputeHash(Encoding.Default.GetBytes(data));
	}
	
	public static string MD5String(string data)
	{
		return Md5ToString(MD5(data));
	}
	
	public static string Md5ToString(byte[] md5)
	{
		str.Length = 0;
		foreach (var b in md5)
		{
			str.Append(b.ToString("x2"));
		}
		return str.ToString();
	}

	public static bool Md5Compare(byte[] code1, byte[]code2)
	{
		for(int i = 0; i < code1.Length; ++i)
		{
			if(code1[i] != code2[i])
				return false;
		}
		return true;
	}
}
