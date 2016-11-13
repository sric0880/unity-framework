using UnityEngine;

public class GUIConsolePageNetwork : GUIConsolePage
{
    private bool useMosServer;
    private bool useTestAccount;
    private string testAccount = "";

    public GUIConsolePageNetwork(string name)
    {
        this.PageName = name;
    }

    public override void Start()
    {
        useTestAccount = (PlayerPrefs.GetInt("usetest") == 1);
        useMosServer = (PlayerPrefs.GetInt("useTestServer") == 1);
        testAccount = PlayerPrefs.GetString("testaccount");
    }

	public override void Exit()
	{
	}

    public override void OnGUI()
    {
        GUILayout.BeginVertical();

        bool temp_useTestAccount = GUILayout.Toggle(useTestAccount, "使用测试号");
        if (temp_useTestAccount != useTestAccount)
        {
            useTestAccount = temp_useTestAccount;
            PlayerPrefs.SetInt("usetest", useTestAccount ? 1 : 0);
        }

        if (useTestAccount)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("请输入测试号：");
            testAccount = GUILayout.TextField(testAccount, GUILayout.Width(100));
            GUILayout.EndHorizontal();
        }
        bool temp_useMosServer = GUILayout.Toggle(useMosServer, "使用mos服务器");
        if (temp_useMosServer != useMosServer)
        {
            useMosServer = temp_useMosServer;
            PlayerPrefs.SetInt("useTestServer", useMosServer ? 1 : 0);
        }
        GUILayout.EndVertical();
    }
}
