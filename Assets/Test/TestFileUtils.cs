using UnityEngine;
using System.Collections;

public class TestFileUtils : MonoBehaviour {

	void Start()
	{
		Log.Init(Log.Tag.Verbose, true, false);
		TestFileUtilsExternalPasses();
		TestFileUtilsInternalPasses();
	}

	private void TestWriteFile()
	{
		byte[] data = new byte[5] {1, 2, 3, 4, 5};
		FileUtils.WriteToFile("dir1/file1.txt", data);
		FileUtils.WriteToFile("file2.txt", "xxxxxxxx");
		FileUtils.WriteToFile("file3.txt", new string[3]{"111", "2222", "44444"});
	}

	void IsTrue(bool assert)
	{
		if (!assert)
		{
			Debug.LogError("expect true, but false");
		}
	}

	void IsFalse(bool assert)
	{
		if (assert)
		{
			Debug.LogError("expect false, but true");
		}
	}

	void AreEqual(int v1, int v2)
	{
		if (v1 != v2)
		{
			Debug.LogErrorFormat("expect {0}, but {1}", v1, v2);
		}
	}

	void AreEqual(string v1, string v2)
	{
		if (v1 != v2)
		{
			Debug.LogErrorFormat("expect {0}, but {1}", v1, v2);
		}
	}

	void TestFileUtilsInternalPasses()
	{
		Debug.Log("1");
		IsTrue(FileUtils.IsFileExist("file1"));
		Debug.Log("2");
		IsFalse(FileUtils.IsFileExist("file1_not_exists"));

		Debug.Log("3");
		byte[] data = FileUtils.GetBytesFromFile("file1");
		Debug.Log("4");
		ulong size = FileUtils.GetFileSize("file1");

		Debug.Log("5");
		AreEqual(data.Length, (int)size);
		Debug.Log("6");
		AreEqual(data.Length, 18);

		Debug.Log("7");
		data = FileUtils.GetBytesFromFile("file_not_exists");
		Debug.Log("8");
		if (data != null)
		{
			Debug.LogError("expect null, but not");
		}

		Debug.Log("9");
		data = FileUtils.GetBytesFromFile("dir/file2");
		Debug.Log("10");
		AreEqual(data.Length, 5);

		Debug.Log("11");
		IsTrue(FileUtils.IsDirectoryExist("dir"));
		Debug.Log("12");
		IsTrue(FileUtils.IsDirectoryExist("dir/"));
		Debug.Log("13");
		IsFalse(FileUtils.IsDirectoryExist("/dir"));
		Debug.Log("14");
		FileUtils.GetBytesFromFileAsync("file_not_exists", (_bytes)=>{
			Debug.Log("141");
			if (_bytes != null)
			{
				Debug.LogError("bytes read from file not exists should be null");
			}
		});
		FileUtils.GetBytesFromFileAsync("file1", (_bytes)=>{
			Debug.Log("142");
			AreEqual(_bytes.Length, 18);
		});
		Debug.Log("15");
		FileUtils.GetStringFromFileAsync("dir/file2", (content)=>{
			Debug.Log("151");
			AreEqual("gwwww", content);
		});
		Debug.Log("16");
		string ct = FileUtils.GetStringFromFile("dir/file2");
		AreEqual("gwwww", ct);
		Debug.Log("Test Success");
	}

	public void TestFileUtilsExternalPasses() {
		// Use the Assert class to test conditions.
		Debug.Log("1");
		TestWriteFile();
		Debug.Log("2");
		IsTrue(FileUtils.IsFileExist("file2.txt"));
		Debug.Log("3");
		IsTrue(FileUtils.IsFileExist("dir1/file1.txt"));

		Debug.Log("4");
		var bytes = FileUtils.GetBytesFromFile("file2.txt");
		AreEqual(bytes.Length, 8);

		FileUtils.GetBytesFromFileAsync("file2.txt", (_bytes)=>{
			Debug.Log("41");
			AreEqual(_bytes.Length, 8);
		});

		Debug.Log("5");
		bytes = FileUtils.GetBytesFromFile("dir1/file1.txt");
		AreEqual(bytes.Length, 5);

		Debug.Log("51");
		var strArr = FileUtils.GetStringArrayFromFile("file3.txt");
		var iter = strArr.GetEnumerator();
		iter.MoveNext();
		AreEqual("111", iter.Current);
		iter.MoveNext();
		AreEqual("2222", iter.Current);
		iter.MoveNext();
		AreEqual("44444", iter.Current);

		FileUtils.GetStringFromFileAsync("file3.txt", (content)=>{
			Debug.Log("52");
			Debug.Log(content);
		});

		Debug.Log("6");
		IsTrue(FileUtils.IsDirectoryExist("dir1"));
		IsTrue(FileUtils.IsDirectoryExist("dir1/"));
		Debug.Log("7");
		IsFalse(FileUtils.IsDirectoryExist("dir2"));
		Debug.Log("8");
		FileUtils.CreateDirectoryIfNeed("dir2");
		Debug.Log("9");
		IsTrue(FileUtils.IsDirectoryExist("dir2"));
		Debug.Log("10");
		IsFalse(FileUtils.IsFileExist("file2.dat"));
		Debug.Log("11");
		FileUtils.RemoveFile("file2.txt");
		FileUtils.RemoveFile("file2.dat");
		FileUtils.RemoveFile("file3.txt");
		FileUtils.RemoveDir("dir2", true);
		FileUtils.RemoveDir("dir1", true);
		Debug.Log("Test success");
	}
}
