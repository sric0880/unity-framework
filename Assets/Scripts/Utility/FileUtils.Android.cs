#if !UNITY_EDITOR && UNITY_ANDROID
//#if UNITY_ANDROID
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FullSerializer;

/// <summary>
/// File and directory operation utility
/// </summary>
public static partial class FileUtils
{
	[DllImport("uu")]
	private static extern int getContents(string filename, StringBuilder buffer, ref int len);
	[DllImport("uu")]
	private static extern bool isFileExistExternal(string filepath);
	[DllImport("uu")]
	private static extern bool isFileExistInternal(string filename);
	[DllImport("uu")]
	private static extern bool isDirectoryExistExternal(string dirPath);
	[DllImport("uu")]
	private static extern bool isDirectoryExistInternal(string dirPath);

	/// <summary>
	/// Gets the full path for filename through the search path.
	/// </summary>
	/// <returns>The full path for filename. If not exists returns null.</returns>
	/// <param name="filename">filename with extension and without path</param>
	public static string GetFullPathForFilename(string filename)
	{
		if (string.IsNullOrEmpty(filename))
		{
			Log.Error("filename is null or empty");
			return null;
		}
		string rPath;
		if (filenameLookupDict.TryGetValue(filename, out rPath))
		{
			if (string.IsNullOrEmpty(rPath))
			{
				Log.Error("filename {0} find full path is null or empty", filename);
				return null;
			}
		}
		else
		{
			rPath = filename;
		}
		//External folder first
		string fullpath = Path.Combine(externalFolder, rPath);
		if (isFileExistExternal(fullpath))
		{
			return fullpath;
		}
		else
		{
			return isFileExistInternal(rPath) ? rPath : null;
		}
	}

	public static bool IsDirectoryExist(string pathname)
	{
		if (!string.IsNullOrEmpty(pathname))
		{
			if (!IsAbsolutePath(pathname))
			{
				if (isDirectoryExistExternal(Path.Combine(externalFolder, pathname)))
					return true;
				if (isDirectoryExistInternal(pathname))
					return true;
			}
			else
			{
				isDirectoryExistExternal(pathname);
			}
		}
		return false;
	}

	public static bool IsAbsolutePath(string filename)
	{
		return filename[0] == '/';
	}

	/// <summary>
	/// Opens the readstream.
	/// </summary>
	/// <returns>FileStream</returns>
	/// <param name="filename">Filename, maybe fullpath.</param>
	public static Stream OpenRead(string filename)
	{
		if (string.IsNullOrEmpty(filename))
		{
			return null;
		}
		if (IsAbsolutePath(filename))
		{
			if (!isFileExistExternal(filename))
				return null;
		}
		else
		{
			filename = GetFullPathForFilename(filename);
		}
		if (filename != null)
		{
			if (IsAbsolutePath(filename))
			{
				return File.OpenRead(filename);
			}
			else
			{
				//return new MemoryStream(getContents(filename, ));
				return new MemoryStream();
			}
		}
		return null;
	}

	//TODO: 
	public static long GetFileSize(string filename)
	{
		string fullpath = GetFullPathForFilename(filename);
		if (fullpath == null)
		{
			return 0;
		}
		var file = new FileInfo(fullpath);
		return file.Length;
	}
}

#endif