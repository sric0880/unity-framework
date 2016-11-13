using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GUIConsolePageLog : GUIConsolePage
{
	Vector2 LogListScrollPosition;
	private MemoryLog memoryLog;
	private const int SHOW_MAX_LINE_NUM = 200;

	bool ShowTimes = true;

	string FilterRegex = "";

	bool[] showTags = new bool[7] { true,true,true,true,true,true,true };
	int[] showTagsNum = new int[7];
	private readonly GUIStyle contentStyle = new GUIStyle()
	{
		fontSize = 15,
		normal = new GUIStyleState { textColor = Color.white },
		wordWrap = true,
	};

	public GUIConsolePageLog(string name)
	{
		this.PageName = name;
	}

	public override void Start()
	{
	}

	public override void OnGUI()
	{
		if (memoryLog == null)
		{
			memoryLog = Log.CloneMemoryLog();
			Log.Tag tag = Log.Tag.Error;
			while (tag >= Log.logPriority)
			{
				showTagsNum[(int)tag] = memoryLog.Count(tag);
				--tag;
			}
			Log.OnLogEvent += OnLogToConsolePage;
		}
		GUILayout.BeginVertical(GUILayout.Height(GUI.skin.window.padding.top), GUILayout.MinHeight(100));
		DrawToolbar();
		DrawFilter();
		DrawLogList();
		GUILayout.EndVertical();
	}

	void DrawToolbar()
	{
		GUILayout.BeginHorizontal();
		if (ButtonClamped("Clear"))
		{
			Clear();
		}
		ShowTimes = ToggleClamped(ShowTimes, "Show Times");

		GUILayout.FlexibleSpace();

		Log.Tag tag = Log.Tag.Error;
		while (tag >= Log.logPriority)
		{
			showTags[(int)tag] = ToggleClamped(showTags[(int)tag], tag.ToString()+showTagsNum[(int)tag].ToString());
			--tag;
		}
		GUILayout.EndHorizontal();
	}

	void Clear()
	{
		if (memoryLog != null)
		{
			memoryLog.Clear();
		}
		for (int i = 0; i < showTagsNum.Length; ++i)
		{
			showTagsNum[i] = 0;
		}
	}

	void DrawFilter()
	{
		GUILayout.BeginHorizontal();
		LabelClamped("Filter Regex");
		var filterRegex = GUILayout.TextArea(FilterRegex);
		if (ButtonClamped("Clear"))
		{
			filterRegex = "";
			GUIUtility.keyboardControl = 0;
			GUIUtility.hotControl = 0;
		}
		if (filterRegex != FilterRegex)
		{
			FilterRegex = filterRegex;
		}

		GUILayout.EndHorizontal();
	}

	/// <summary>
	/// Draws the main log panel
	/// </summary>
	public void DrawLogList()
	{
		LogListScrollPosition = GUILayout.BeginScrollView(LogListScrollPosition, GUILayout.Height(460));

		System.Text.RegularExpressions.Regex filterRegex = null;

		if (!String.IsNullOrEmpty(FilterRegex))
		{
			filterRegex = new System.Text.RegularExpressions.Regex(FilterRegex);
		}

		foreach (var log in memoryLog.ReverseLogs(SHOW_MAX_LINE_NUM).Reverse())
		{
			if (ShouldShowLog(filterRegex, log))
			{
				var oldTagColor = GUI.color;
				GUI.color = GetColor(log.tag);
				if (ShowTimes)
				{
					GUILayout.Label(string.Format(Log.LOG_FORMAT, log.tag, log.time, log.msg), contentStyle, GUILayout.MaxWidth(420));
				}
				else
				{
					GUILayout.Label(string.Format(Log.LOG_FORMAT_NOTIME, log.tag, log.msg), contentStyle, GUILayout.MaxWidth(420));
				}
				GUI.color = oldTagColor;
			}
		}
		GUILayout.EndScrollView();
	}

	private Color GetColor(Log.Tag tag)
	{
		switch (tag)
		{
			case Log.Tag.Error:
				return Color.red;

			case Log.Tag.Warning:
				return Color.yellow;

			case Log.Tag.Info:
				return Color.green;

			case Log.Tag.Debug:
				return Color.white;

			case Log.Tag.Profile:
				return Color.blue;

			case Log.Tag.Verbose:
				return Color.gray;

			default:
				return Color.gray;
		}
	}

	public override void Exit()
	{
		if (memoryLog != null)
		{
			Log.OnLogEvent -= OnLogToConsolePage;
			memoryLog = null;
		}
	}

	private void OnLogToConsolePage(LogLine log)
	{
		if (memoryLog != null)
		{
			memoryLog.Add(log);
			++showTagsNum[(int)log.tag];
		}
	}

	bool ButtonClamped(string text)
	{
		return GUILayout.Button(text, GUILayout.MaxWidth(100));
	}

	bool ToggleClamped(bool state, string text)
	{
		return GUILayout.Toggle(state, text, GUILayout.MaxWidth(100));
	}

	bool ToggleClamped(bool state, GUIContent content)
	{
		return GUILayout.Toggle(state, content, GUILayout.MaxWidth(100));
	}

	void LabelClamped(string text)
	{
		GUILayout.Label(text, GUILayout.MaxWidth(100));
	}

	bool ShouldShowLog(System.Text.RegularExpressions.Regex regex, LogLine log)
	{
		if (showTags[(int)log.tag] && (regex == null || regex.IsMatch(log.msg)))
		{
			return true;
		}
		return false;
	}
}