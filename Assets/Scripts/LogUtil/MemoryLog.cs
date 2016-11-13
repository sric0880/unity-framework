using System.Collections.Generic;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class MemoryLog
{
	public readonly List<LogLine> logs = new List<LogLine>();

	public void Add(LogLine line) { logs.Add(line); }

	public void Clear() { logs.Clear(); }

	public void MoveMemoryLogToFile(StreamWriter stream)
	{
		if (stream != null)
		{
			for (int i = 0; i < logs.Count; ++i)
			{
				stream.Write(Log.LOG_FORMAT, logs[i].tag, logs[i].time, logs[i].msg);
			}
			stream.Flush();
		}
	}

	public IEnumerable<LogLine> ReverseLogs(int maxLine)
	{
		int logLines = 0;
		for (int index = logs.Count - 1; index >= 0 && logLines < maxLine; --index)
		{
			yield return logs[index];
			++logLines;
		}
	}

	public void CopyFromMemoryLog(MemoryLog memoryLog)
	{
		if (memoryLog == null)
			return;
		var _logs = memoryLog.logs.ToArray();
		if (_logs != null)
		{
			foreach (var log in _logs)
				logs.Add(log);
		}
	}

	public void CopyFromFile(string logfile)
	{
		List<LogLine> _logs = new List<LogLine>();
		using (var f = new StreamReader(FileUtils.OpenRead(logfile)))
		{
			string readLine;
			while ((readLine = f.ReadLine()) != null)
			{
				LogLine newlog;
				if (ParseLogLine(readLine, out newlog) || logs.Count == 0)
				{
					logs.Add(newlog);
				}
				else
				{
					LogLine line = logs[logs.Count - 1];
					line.msg += "\r\n" + readLine;
					logs[logs.Count - 1] = line;
				}
			}
		}
		foreach (var log in _logs)
			logs.Add(log);
	}

	public int Count(Log.Tag tag)
	{
		int total = 0;
		for (int index = logs.Count - 1; index >= 0; --index)
		{
			var log = logs[index];
			if (log.tag == tag)
				++total;
		}
		return total;
	}

	private static readonly Regex regLog = new Regex(@"(\w+) (\d+):(\d+):(\d+).(\d+)\[(\d+)\] (.*)");
	private const int REG_TAG = 1;
	private const int REG_H = 2;
	private const int REG_MM = 3;
	private const int REG_SS = 4;
	private const int REG_FFFFFFF = 5;
	private const int REG_MSG = 6;

	private bool ParseLogLine(string fileline, out LogLine line)
	{
		var m = regLog.Match(fileline);
		if (m.Success)
		{
			line.msg = m.Groups[REG_MSG].Value;
			line.tag = (Log.Tag)Enum.Parse(typeof(Log.Tag), m.Groups[REG_TAG].Value);
			line.time = new DateTime(
				int.Parse(m.Groups[REG_H].Value) * TimeSpan.TicksPerHour +
				int.Parse(m.Groups[REG_MM].Value) * TimeSpan.TicksPerMinute +
				int.Parse(m.Groups[REG_SS].Value) * TimeSpan.TicksPerSecond +
				(long)(float.Parse("0." + m.Groups[REG_FFFFFFF].Value) * TimeSpan.TicksPerSecond)
				);
			return true;
		}
		else
		{
			line.msg = fileline;
			line.tag = Log.Tag.Warning;
			line.time = new DateTime();
			return false;
		}
	}
}
