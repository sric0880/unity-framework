using System;

public static class StringUtils
{
    public static string BytesToString(byte[] data)
    {
        return System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
    }

    public static string[] BytesToStringArray(byte[] data)
    {
        string allString = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
		return allString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
    }
}