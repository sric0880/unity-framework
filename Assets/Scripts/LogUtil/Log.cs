using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public struct LogLine
{
	public Log.Tag tag;
	public DateTime time;
	public string msg;
}

public static class Log
{
	public enum Tag
	{
		Verbose = 1,
		Profile = 2,
		Debug = 3, 
		Info = 4,
		Warning = 5,
		Error = 6,
	};
	public const string LOG_FORMAT = "{0} {1:H:mm:ss.fffffff} {2}\r\n";
	public const string LOG_FORMAT_NOTIME = "{0} {1}\r\n";

	public static MemoryLog memoryLog = new MemoryLog();
	public static Tag logPriority = Tag.Debug;
	public static event Action<LogLine> OnLogEvent;

	public static void Verbose(string msg, params object[] args) { LogImpl(Tag.Verbose, msg, args); }
	public static void Profile(string msg, params object[] args) { LogImpl(Tag.Profile, msg, args); }
	public static void Debug(string msg, params object[] args) { LogImpl(Tag.Debug, msg, args); }
	public static void Info(string msg, params object[] args) { LogImpl(Tag.Info, msg, args); }
	public static void Warning(string msg, params object[] args) { LogImpl(Tag.Warning, msg, args); }
	public static void Error(string msg, params object[] args) { LogImpl(Tag.Error, msg, args); }

	private static void LogImpl(Tag tag, string msg, params object[] args)
	{
		if (tag < logPriority)
			return;

		DateTime now = DateTime.Now;

		string formated = msg == null ? "null" : msg;
		if (args != null && args.Length > 0)
		{
			for (int i = 0; i < args.Length; ++i)
			{
				if (args[i] == null)
					args[i] = "null";
			}
			formated = string.Format(formated, args);
		}

		LogLine logLine = new LogLine { time = now, msg = formated, tag = tag };

		if (memoryLog != null)
		{
			memoryLog.Add(logLine);
		}

		if (OnLogEvent != null)
		{
			OnLogEvent(logLine);
		}
	}

	public static void Init(Tag logPriority, bool logToUnityConsole, bool logToFile, string logFile)
	{
		Application.logMessageReceived += OnApplicationLog;
		Log.logPriority = logPriority;
		if (logToUnityConsole)
			DoLogToUnityConsole(true);
		if (logToFile)
			DoLogToFile(true, logFile);
	}

	public static void OnApplicationLog(string condition, string trace, LogType type)
	{
		string traceToFile = trace.Replace("\n", "\r\n");
		switch (type)
		{
			case LogType.Log:
				Log.Debug("{0}\r\n{1}", condition, traceToFile);
				break;
			case LogType.Warning:
				Log.Warning("{0}\r\n{1}", condition, traceToFile);
				break;
			case LogType.Error:
			case LogType.Exception:
			case LogType.Assert:
				Log.Error("{0}\r\n{1}", condition, traceToFile);
				break;
			default:
				break;
		}
	}

	#region Log to unity console
	public static void DoLogToUnityConsole(bool done)
	{
		if (done) OnLogEvent += OnLogToUnityConsole;
		else OnLogEvent -= OnLogToUnityConsole;
	}

	public static void OnLogToUnityConsole(LogLine log)
	{
		string message = string.Format(LOG_FORMAT, log.tag, log.time, log.msg);
		if (log.tag == Log.Tag.Error) UnityEngine.Debug.LogError(message);
		else if (log.tag == Log.Tag.Warning) UnityEngine.Debug.LogWarning(message);
		else UnityEngine.Debug.Log(message);
	}
	#endregion

	#region Log to file
	private static StreamWriter stream;
	private const int MAX_BACKUP_LOGFILE_NUM = 5;

	public static void DoLogToFile(bool done, string logfile = null)
	{
		if (done)
		{
			if (stream != null)
			{
				Log.Warning("Log.DoLogToFile can be invoked only once");
				return;
			}

			try
			{
				BackupPrevLogs(logfile, MAX_BACKUP_LOGFILE_NUM);
				stream = new StreamWriter(FileUtils.OpenWrite(logfile));
			}
			catch (Exception e)
			{
				Log.Warning("Can not open file {0} for log, {1}", logfile, e);
				return;
			}

			if (memoryLog != null)
			{
				memoryLog.MoveMemoryLogToFile(stream);
				memoryLog = null;
			}
			OnLogEvent += OnLogToFile;
		}
		else OnLogEvent -= OnLogToFile;
	}

	public static void OnLogToFile(LogLine log)
	{
		if (stream != null)
		{
			stream.Write(LOG_FORMAT, log.tag, log.time, log.msg);
			stream.Flush();
		}
	}

	private static void BackupPrevLogs(string file, int max)
	{
		string finalLog = BackupLogfileName(file, max);
		FileUtils.RemoveFile(finalLog);

		for (int i = max - 1; i >= 0; --i)
		{
			string src = BackupLogfileName(file, i);
			if (File.Exists(src))
			{
				File.Move(src, BackupLogfileName(file, i + 1));
			}
		}
	}

	private static string BackupLogfileName(string file, int index)
	{
		if (index > 0)
			return FileUtils.ReplaceExtension(file, string.Format(".bak{0}.txt", index));
		else
			return file;
	}
	#endregion

	#region Log to LogPageConsole
	public static MemoryLog CloneMemoryLog()
	{
		var _memoryLog = new MemoryLog();

		if (memoryLog != null)
		{
			_memoryLog.CopyFromMemoryLog(memoryLog);
		}
		else
		{
			_memoryLog.CopyFromFile("");
		}
		return _memoryLog;
	}
	#endregion
}
