using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public static partial class FileUtils
{
	public const string export_boot_config_folder = "bootconfigexport";
	public const string binary_config_folder = "bin";
	public static string boot_locale_folder { get { return Replace.R("bootlocale/{locale}", LaunchConfig.boot); } }
	public static string locale_folder { get { return Replace.R("locale/{locale}/dictionary", LaunchConfig.boot); } }
	public static string log_file(int index) { return string.Format("log/log_{0}.txt", index); }
	public static string bundle_folder { get { return string.Format("assets/{0}", PlatformNameUtils.GetPlatformName()); } }
	public static string gamesave(long roleId) { return string.Format("gamesave/{0}", roleId); }

	/// <summary>
	/// FIXME: resources folder root dir
	/// </summary>
	private static string externalEditor = "../resources/";
	private static string externalPlayer = "./";

	public static string externalFolder
	{
		get
		{
			switch (Application.platform)
			{
				case RuntimePlatform.WindowsEditor:
				case RuntimePlatform.OSXEditor:
					return Path.GetFullPath(externalEditor);
				case RuntimePlatform.WindowsPlayer:
				case RuntimePlatform.OSXPlayer:
					return Path.GetFullPath(externalPlayer);
				case RuntimePlatform.Android:
					return Path.GetFullPath(Application.persistentDataPath);
				case RuntimePlatform.IPhonePlayer:
					return Path.GetFullPath(Application.temporaryCachePath);
				default:
					return null;
			}
		}
	}

	/// <summary>
	/// Gets the init data folder using at the time of game launch
	/// using StreamingAssetsPath
	/// </summary>
	/// <value>The init data folder.</value>
	public static string internalFolder
	{
		get
		{
			if (Application.isMobilePlatform && !Application.isEditor)
				return Application.streamingAssetsPath;
			return externalFolder;
		}
	}

	/// <summary>
	/// Gets the full writable path for pathname
	/// </summary>
	/// <returns>The full writable path for pathname.</returns>
	/// <param name="pathname">Pathname: absolute path or relative path begins after /assets.</param>
	public static string GetWritablePathForPathname(string pathname)
	{
		string fullpath = pathname;
		if (!string.IsNullOrEmpty(pathname))
		{
			if (!IsAbsolutePath(pathname))
			{
				fullpath = Path.Combine(externalFolder, pathname);
			}
		}
		return fullpath;
	}

	public static bool IsFileExist(string filename)
	{
		return GetFullPathForFilename(filename) != null;
	}

	public static FileStream OpenWrite(string pathname)
	{
		string fullpath = GetWritablePathForPathname(pathname);
		if (!string.IsNullOrEmpty(fullpath))
		{
			CreateParentDirectoryIfNeed(fullpath);
			return File.OpenWrite(fullpath);
		}
		return null;
	}

	public static void WriteToFile(string pathname, byte[] content)
	{
		var filestream = OpenWrite(pathname);
		if (filestream != null)
		{
			filestream.Write(content, 0, content.Length);
			filestream.Close();
		}
	}

	public static void WriteToFile(string pathname, string content)
	{
		var filestream = OpenWrite(pathname);
		if (filestream != null)
		{
			using (var writer = new StreamWriter(filestream))
			{
				writer.Write(content);
			}
			filestream.Close();
		}
	}

	public static void WriteToFile(string pathname, IEnumerable<string> content)
	{
		var filestream = OpenWrite(pathname);
		if (filestream != null)
		{
			using (var writer = new StreamWriter(filestream))
			{
				foreach (var line in content)
				{
					writer.WriteLine(line);
				}
			}
			filestream.Close();
		}
	}

	// 同步方法
	static string __GetStringFromFile(string filename)
	{
		var stream = File.OpenRead(filename);
		string ret = null;
		if (stream != null)
		{
			using (var reader = new StreamReader(stream, System.Text.Encoding.UTF8))
			{
				ret = reader.ReadToEnd();
			}
			stream.Close();
		}
		return ret;
	}

	public static void GetStringFromFileAsync(string filename, Action<string> callback)
	{
		GetBytesFromFileAsync(filename, (data) =>
		{
			if (callback != null)
			{
				callback(StringUtils.BytesToString(data));
			}
		});
	}

	static IEnumerable<string> __GetStringArrayFromFile(string filename)
	{
		var stream = File.OpenRead(filename);
		if (stream != null)
		{
			using (var reader = new StreamReader(stream, System.Text.Encoding.UTF8))
			{
				while (!reader.EndOfStream)
				{
					yield return reader.ReadLine();
				}
			}
			stream.Close();
		}
	}

	public static void GetStringArrayFromFileAsync(string filename, Action<string[]> callback)
	{
		GetBytesFromFileAsync(filename, (data) =>
		{
			if (callback != null)
			{
				callback(StringUtils.BytesToStringArray(data));
			}
		});
	}

	static byte[] __GetBytesFromFile(string filename)
	{
		var stream = File.OpenRead(filename);
		byte[] ret = null;
		if (stream != null)
		{
			ret = new byte[stream.Length];
			try
			{
				stream.Read(ret, 0, (int)stream.Length);
			}
			catch (IOException e)
			{
				Log.Error("[FileUtils] read bytes from file {0} error, {1}", stream.Name, e.Message);
			}
			finally
			{
				stream.Close();
			}
		}
		return ret;
	}
	//

	struct AsyncState
	{
		public FileStream fileStream;
		public byte[] buffer;
		public System.Action<byte[]> callback;
	}

	// 异步方法
	static void __GetBytesFromFileAsync(string filename, System.Action<byte[]> callback)
	{
		var stream = File.OpenRead(filename);
		if (stream != null)
		{
			AsyncState state = new AsyncState();
			state.fileStream = stream;
			state.callback = callback;
			state.buffer = new byte[stream.Length];
			try
			{
				stream.BeginRead(state.buffer, 0, state.buffer.Length, __OnCompletedRead, state);
			}
			catch (IOException e)
			{
				Log.Error("[FileUtils] async begin read bytes from file {0} error, {1}", stream.Name, e.Message);
			}
		}
	}

	static void __OnCompletedRead(IAsyncResult asyncResult)
	{
		var asyncState = (AsyncState)asyncResult.AsyncState;
		try
		{
			int bytesRead = asyncState.fileStream.EndRead(asyncResult);
			if (bytesRead == asyncState.buffer.Length)
			{
				if (asyncState.callback != null)
				{
					// callback on main thread
					SchedulerUtils.MainThread_Invoke(() => {
						asyncState.callback(asyncState.buffer);
					});
				}
			}
			else
			{
				Log.Error("[FileUtils] async end read bytes from file {0} error", asyncState.fileStream.Name);
			}
		}
		catch (IOException e)
		{
			Log.Error("[FileUtils] async end read bytes from file {0} error, {1}", asyncState.fileStream.Name, e.Message);
		}
		finally
		{
			asyncState.fileStream.Close();
		}
	}

	public static MemoryStream GetMemoryStreamFromFile(string filename)
	{
		byte[] bytes = GetBytesFromFile(filename);
		if (bytes != null)
		{
			return new MemoryStream(bytes);
		}
		return null;
	}

	public static void GetMemoryStreamFromFileAsync(string filename, System.Action<MemoryStream> callback)
	{
		GetBytesFromFileAsync(filename, (bytes) => {
			if (callback != null)
			{
				if (bytes != null) {
					callback(new MemoryStream(bytes));
				} else {
					callback(null);
				}
			}
		});
	}

	public static void CreateParentDirectoryIfNeed(string pathname)
	{
		string fullpath = GetWritablePathForPathname(pathname);
		if (!string.IsNullOrEmpty(fullpath))
		{
			string folder = Path.GetDirectoryName(fullpath);
			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}
		}
	}

	public static void CreateDirectoryIfNeed(string folder)
	{
		string fullpath = GetWritablePathForPathname(folder);
		if (!string.IsNullOrEmpty(fullpath))
		{
			if (!Directory.Exists(fullpath))
			{
				Directory.CreateDirectory(fullpath);
			}
		}
	}

	public static string ReplaceExtension(string filename, string ext)
	{
		int index = filename.LastIndexOf('.');
		string dirname = filename.Substring(0, index == -1 ? filename.Length : index + 1);
		return dirname + ext;
	}

	public static void RemoveFile(string pathname)
	{
		string fullpath = GetWritablePathForPathname(pathname);
		if (!string.IsNullOrEmpty(fullpath))
		{
			if (File.Exists(fullpath))
			{
				File.Delete(fullpath);
			}
		}
	}

	public static void RemoveDir(string folder, bool recursive)
	{
		string fullpath = GetWritablePathForPathname(folder);
		if (!string.IsNullOrEmpty(fullpath))
		{
			if (Directory.Exists(fullpath))
			{
				Directory.Delete(fullpath, recursive);
			}
		}
	}
}