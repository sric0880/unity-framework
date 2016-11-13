using System;
using UnityEngine;
using System.IO;
using System.Text;
using System.Reflection;
using FullSerializer;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static partial class FileUtils
{
	public const string boot_config_folder = "bootconfig";
	public const string export_boot_config_folder = "bootconfigexport";
	public const string raw_config_folder = "config";
	public const string export_raw_config_folder = "configexport";
	public const string binary_config_folder = "bin";
	public static string boot_locale_folder { get { return FormatUtility.Format("bootlocale/{locale}"); } }
	public static string locale_folder { get { return FormatUtility.Format("locale/{locale}/dictionary"); } }
	public const string bundle_folder = "assets";
	public const string log_folder = "log";
	public static string log_file(int index) { return FormatUtility.Format("log/log_{0}.txt", index); }
	public const string lookup_dict_file = "lookup"; //May have many different versions

	private static readonly fsSerializer _serializer = new fsSerializer();
	private static Dictionary<string, string> filenameLookupDict = new Dictionary<string, string>();

	/// <summary>
	/// FIXME: resources folder root dir
	/// </summary>
	private static string externalEditor = "../../resources/com.sric.test";
	private static string externalPlayer = "../com.sric.test";

	public static string externalFolder
	{
		get
		{
			switch (Application.platform)
			{
				case RuntimePlatform.WindowsEditor:
					return Path.GetFullPath(externalEditor);
				case RuntimePlatform.WindowsPlayer:
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

	public static string GetStringFromFile(string filename)
	{
		var stream = OpenRead(filename);
		string ret = string.Empty;
		if (stream != null)
		{
			using (var reader = new StreamReader(stream))
			{
				ret = reader.ReadToEnd();
			}
			stream.Close();
		}
		return ret;
	}

	public static IEnumerable<string> GetStringArrayFromFile(string filename)
	{
		var stream = OpenRead(filename);
		if (stream != null)
		{
			using (var reader = new StreamReader(stream))
			{
				while (!reader.EndOfStream)
				{
					yield return reader.ReadLine();
				}
			}
			stream.Close();
		}
	}

	public static byte[] GetBytesFromFile(string filename)
	{
		var stream = OpenRead(filename);
		byte[] ret = null;
		if (stream != null)
		{
			ret = new byte[stream.Length];
			stream.Read(ret, 0, (int)stream.Length);
			stream.Close();
		}
		return ret;
	}

	public static void GetObjectFromJsonFile<T>(string filename, ref T obj)
	{
		fsData json_data;
		fsResult res = fsJsonParser.Parse(GetStringFromFile(filename), out json_data);
		if (res.Failed)
		{
			Log.Error("Error parse json from file: {0}", filename);
			return;
		}
		_serializer.TryDeserialize<T>(json_data, ref obj).AssertSuccess();
	}

	public static void GetObjectFromBinaryFile<T>(string filename, ref T obj)
	{
		var stream = OpenRead(filename);
		if (stream != null)
		{
			var formatter = new BinaryFormatter();
			obj = (T)formatter.Deserialize(stream);
			stream.Close();
		}
	}

	public static void GetFiledValueFromJsonFile(string filename, FieldInfo field)
	{
		fsData json_data;
		fsResult res = fsJsonParser.Parse(GetStringFromFile(filename), out json_data);
		if (res.Failed)
		{
			Log.Error("Error parse json from file: {0}", filename);
			return;
		}
		var value = field.GetValue(null);
		_serializer.TryDeserialize(json_data, field.FieldType, ref value).AssertSuccess();
		field.SetValue(null, value);
	}

	/// <summary>
	/// Loads the filename lookup dictionary from file which is generated manually and pack into the release package.
	/// Filename is the key, relative pathname is then value. Support mutiple values.
	/// Example 1: /assets/bundles/ui/gg_window.bundle, the pair is {gg_window.bundle: bundles/ui/gg_window.bundle}
	/// </summary>
	public static void LoadFilenameLookupDict()
	{
		foreach (var line in GetStringArrayFromFile(lookup_dict_file))
		{
			int index = line.LastIndexOfAny(new char[] { '/', '\\' });
			string filename = line.Substring(index + 1);
			filenameLookupDict.Add(filename, line);
		}
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

	public static void WriteToJsonFile<T>(string pathname, T obj) where T : new()
	{
		fsData data;
		_serializer.TrySerialize(obj, out data).AssertSuccess();
		byte[] content = new UTF8Encoding(true).GetBytes(fsJsonPrinter.PrettyJson(data));
		WriteToFile(pathname, content);
	}

	public static void WriteToJsonFile(string pathname, FieldInfo fieldInfo)
	{
		fsData data;
		_serializer.TrySerialize(fieldInfo.FieldType, fieldInfo.GetValue(null), out data).AssertSuccess();
		byte[] content = new UTF8Encoding(true).GetBytes(fsJsonPrinter.PrettyJson(data));
		WriteToFile(pathname, content);
	}

	public static void WriteToBinaryFile(string pathname, object obj)
	{
		var filestream = OpenWrite(pathname);
		if (filestream != null)
		{
			var formatter = new BinaryFormatter();
			formatter.Serialize(filestream, obj);
			filestream.Close();
		}
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
}