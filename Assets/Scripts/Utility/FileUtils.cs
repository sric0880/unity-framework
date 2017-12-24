#if UNITY_EDITOR || UNITY_IOS || UNITY_STANDALONE
// #if UNITY_IOS
using System.Collections.Generic;
using System.IO;

/// <summary>
/// File and directory operation utility
/// </summary>
public static partial class FileUtils {
	/// <summary>
	/// Gets the full path for filename through the search path.
	/// </summary>
	/// <returns>The full path for filename. If not exists returns null.</returns>
	/// <param name="filename">filename with extension and without path</param>
	public static string GetFullPathForFilename (string filename) {
		if (string.IsNullOrEmpty (filename)) {
			Log.Error ("[FileUtils] filename is null or empty");
			return null;
		}
		//External folder first
		string fullpath = Path.Combine (externalFolder, filename);
		if (File.Exists (fullpath)) {
			return fullpath;
		} else {
			fullpath = Path.Combine (internalFolder, filename);
			return File.Exists (fullpath) ? fullpath : null;
		}
	}

	public static bool IsDirectoryExist (string pathname) {
		string fullpath = pathname;
		if (!string.IsNullOrEmpty (pathname)) {
			if (!IsAbsolutePath (pathname)) {
				fullpath = Path.Combine (externalFolder, pathname);
				if (Directory.Exists (fullpath))
					return true;
				fullpath = Path.Combine (internalFolder, pathname);
				if (Directory.Exists (fullpath))
					return true;
			} else {
				Directory.Exists (pathname);
			}
		}
		return false;
	}

	public static bool IsAbsolutePath (string filename) {
		return Path.IsPathRooted (filename);
	}

	/// <summary>
	/// get the real file read path.
	/// </summary>
	/// <returns>file path for reading</returns>
	/// <param name="filename">Filename, maybe fullpath.</param>
	static string __GetReadFilePath (string filename) {
		if (string.IsNullOrEmpty (filename)) {
			return null;
		}
		if (IsAbsolutePath (filename)) {
			if (!File.Exists (filename))
				return null;
		} else {
			filename = GetFullPathForFilename (filename);
		}
		if (filename == null) {
			Log.Error ("[FileUtils] file not found both internal or external storage");
		}
		return filename;
	}

	public static string GetStringFromFile (string filename) {
		filename = __GetReadFilePath (filename);
		if (filename != null) {
			return __GetStringFromFile (filename);
		}
		return null;
	}

	public static IEnumerable<string> GetStringArrayFromFile (string filename) {
		filename = __GetReadFilePath (filename);
		if (filename != null) {
			return __GetStringArrayFromFile (filename);
		}
		return null;
	}

	public static byte[] GetBytesFromFile (string filename) {
		filename = __GetReadFilePath (filename);
		if (filename != null) {
			return __GetBytesFromFile (filename);
		}
		return null;
	}

	public static void GetBytesFromFileAsync (string filename, System.Action<byte[]> callback) {
		filename = __GetReadFilePath (filename);
		if (filename != null) {
			__GetBytesFromFileAsync (filename, callback);
		} else {
			if (callback != null) {
				callback (null);
			}
		}
	}

	public static ulong GetFileSize (string filename) {
		string fullpath = GetFullPathForFilename (filename);
		if (fullpath == null) {
			return 0;
		}
		var file = new FileInfo (fullpath);
		return (ulong) file.Length;
	}
}

#endif