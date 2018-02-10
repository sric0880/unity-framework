using UnityEngine;
using System.Collections.Generic;

public class GUIConsole : MonoBehaviour {
	//GUI
    private bool toggleConsolePage = true;
    int selected = 0;
	string[] pageTitles;

    //Data
    private List<GUIConsolePage> pages;
	private List<GUIConsoleHeader> headers;

	void Start () {
		enabled = UF.Managers.ConfigManager.Instance.launch.boot.show_console;
        pages = new List<GUIConsolePage>();
		headers = new List<GUIConsoleHeader>();

        pages.Add(new GUIConsolePageConfig("Config"));
        pages.Add(new GUIConsolePageNetwork("Network"));
        pages.Add(new GUIConsolePageSystemInfo("SystemInfo"));
		pages.Add(new GUIConsolePageLog("Log"));

		headers.Add(new GUIConsoleHeaderFps());

        pageTitles = new string[pages.Count];
        for (int i = 0; i < pages.Count; ++i)
        {
            pageTitles[i] = pages[i].PageName;
            pages[i].Start();
        }
	}

	void Exit()
	{
		pages[selected].Exit();
	}

    void Update()
    {
		for (int i = 0; i < headers.Count; ++i)
		{
			headers[i].OnUpdate();	
		}
    }

    void OnGUI()
    {
		ResizeGUIMatrix();
		SetGUIStyle();

		GUILayout.BeginVertical("box");
		GUILayout.Space(10);

		GUILayout.BeginHorizontal();
		toggleConsolePage = GUILayout.Toggle(toggleConsolePage, "开关");
		for (int i = 0; i < headers.Count; ++i)
		{
			headers[i].OnGUI();
		}
		if (GUILayout.Button("关闭", GUILayout.Width(60), GUILayout.Height(40)))
		{
			Exit();
			enabled = false;
		}
		GUILayout.EndHorizontal();
		if (toggleConsolePage)
		{
			int _selected = GUILayout.SelectionGrid(selected, pageTitles, pageTitles.Length, GUILayout.Width(pages.Count * 100), GUILayout.Height(50));
			if (_selected != selected)
			{
				Exit();
				selected = _selected;
			}
			pages[selected].OnGUI();
		}
		else
		{
			Exit();
		}

		GUILayout.EndVertical();

		GUI.matrix = Matrix4x4.identity;
    }

    void ResizeGUIMatrix()
    {
        Vector2 ratio = new Vector2(Screen.width / 960.0f, Screen.height / 640.0f);
        Matrix4x4 guiMatrix = Matrix4x4.identity;
        guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
        GUI.matrix = guiMatrix;
    }

	void SetGUIStyle()
	{
		GUI.skin.toggle.hover.textColor = new Color(1, 0, 0, 1);
		GUI.skin.label.fontSize = 15;
		GUI.skin.label.alignment = TextAnchor.MiddleLeft;
	}
}