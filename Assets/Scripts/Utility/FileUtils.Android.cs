#if !UNITY_EDITOR && UNITY_ANDROID
// #if UNITY_ANDROID
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UniRx;

/// <summary>
/// File and directory operation utility
/// </summary>
public static partial class FileUtils {
	[DllImport ("uu")]
	private static extern System.IntPtr getFileBytes (string filename, ref ulong size);
	[DllImport ("uu")]
	public static extern void freeFileBytes (System.IntPtr obj);
	[DllImport ("uu")]
	private static extern bool isFileExistInternal (string filename);
	[DllImport ("uu")]
	private static extern bool isDirectoryExistInternal (string dirPath);
	[DllImport ("uu")]
	private static extern ulong getFileSize (string filename);
	// public delegate void OnBytesRead (System.IntPtr filename, System.IntPtr data, ulong size);
	// private static extern void getFileBytesAsync (string filename, [MarshalAs (UnmanagedType.FunctionPtr)] OnBytesRead callback);

	// [MonoPInvokeCallback(typeof(OnBytesRead))]
	// static void __CallbackFromC (System.IntPtr filenamePtr, System.IntPtr dataPtr, ulong len) {
		// callback on main thread
		// SchedulerUtils.MainThread_Invoke (() => {
			// Log.Error(filenamePtr.ToInt32().ToString());
			// string filename = Marshal.PtrToStringAuto(filenamePtr);
			// Log.Error(filename);
			// if (callbacks.ContainsKey (filename)) {
				// var callback = callbacks[filename];
				// if (callback != null) {
				// 	callback(__GetBytesFromDataPtr (dataPtr, len));
				// }
				// callbacks.Remove(filename);
			// }
			// else{
			// 	Log.Error("Not contains the filename: {0}", filename);
			// }
	// 	});
	// }

	static byte[] __GetBytesFromDataPtr (System.IntPtr dataPtr, ulong size) {
		if (dataPtr == System.IntPtr.Zero || size == 0) {
			Log.Error("[FileUtils] getFileBytesAsync return null");
			return null;
		}
		byte[] bytes = new byte[size];
		Marshal.Copy (dataPtr, bytes, 0, bytes.Length);
		freeFileBytes (dataPtr);
		return bytes;
	}

	static byte[] __NativeGetBytes (string filename) {
		ulong size = 0;
		var dataPtr = getFileBytes (filename, ref size);
		return __GetBytesFromDataPtr (dataPtr, size);
	}

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
			return isFileExistInternal (filename) ? filename : null;
		}
	}

	public static bool IsDirectoryExist (string pathname) {
		if (!string.IsNullOrEmpty (pathname)) {
			if (!IsAbsolutePath (pathname)) {
				if (Directory.Exists (Path.Combine (externalFolder, pathname)))
					return true;
				if (isDirectoryExistInternal (pathname))
					return true;
			} else {
				Directory.Exists (pathname);
			}
		}
		return false;
	}

	public static bool IsAbsolutePath (string filename) {
		return filename[0] == '/';
	}

	/// <summary>
	/// get the real file read path.
	/// </summary>
	/// <param name="filename">return real read filepath</param>
	/// <returns>true if file in external storage</returns>
	static bool __GetReadFilePath (ref string filename) {
		if (string.IsNullOrEmpty (filename)) {
			filename = null;
			return true;
		}
		if (IsAbsolutePath (filename)) {
			if (!File.Exists (filename)) {
				filename = null;
				return true;
			}
		} else {
			filename = GetFullPathForFilename (filename);
		}
		if (filename != null) {
			if (IsAbsolutePath (filename)) {
				return true;
			} else {
				return false;
			}
		} else {
			Log.Error("[FileUtils] file not found both internal or external storage");
		}
		return true;
	}

	public static string GetStringFromFile (string filename) {
		bool isInExternalStorage = __GetReadFilePath (ref filename);
		if (filename != null) {
			if (isInExternalStorage) {
				return __GetStringFromFile (filename);
			} else {
				byte[] data = __NativeGetBytes (filename);
				if (data != null) {
					return StringUtils.BytesToString (data);
				}
			}
		}
		return null;
	}

	public static IEnumerable<string> GetStringArrayFromFile (string filename) {
		bool isInExternalStorage = __GetReadFilePath (ref filename);
		if (filename != null) {
			if (isInExternalStorage) {
				return __GetStringArrayFromFile (filename);
			} else {
				byte[] data = __NativeGetBytes (filename);
				if (data != null) {
					return StringUtils.BytesToStringArray (data);
				}
			}
		}
		return null;
	}

	public static byte[] GetBytesFromFile (string filename) {
		bool isInExternalStorage = __GetReadFilePath (ref filename);
		if (filename != null) {
			if (isInExternalStorage) {
				return __GetBytesFromFile (filename);
			} else {
				return __NativeGetBytes (filename);
			}
		}
		return null;
	}

	public static void GetBytesFromFileAsync (string filename, System.Action<byte[]> callback) {
		bool isInExternalStorage = __GetReadFilePath (ref filename);
		if (filename != null) {
			if (isInExternalStorage) {
				__GetBytesFromFileAsync (filename, callback);
			} else {
				Observable.Start<byte[]>(()=>{
					return __NativeGetBytes(filename);
				})
				.ObserveOnMainThread()
				.Subscribe(callback);
			}
		} else{
			if (callback != null){
				callback(null);
			}
		}
	}

	public static ulong GetFileSize (string filename) {
		bool isInExternalStorage = __GetReadFilePath (ref filename);
		if (filename != null) {
			if (isInExternalStorage) {
				var file = new FileInfo (filename);
				if (file != null) return (ulong) file.Length;
			} else {
				return getFileSize (filename);
			}
		}
		return 0;
	}
}

#endif