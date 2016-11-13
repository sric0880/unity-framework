using UnityEngine;

public class GUIConsolePageConfig : GUIConsolePage
{
    private bool sceneOfDusk;
    private bool fx;
    private float maxSpeed;
    private bool flySkyHigh;
    private bool useNewbieGuide;
    private int skillIndex;
    private int soulIndex;

    public GUIConsolePageConfig(string name)
    {
        this.PageName = name;
    }

    public override void Start()
    {
        useNewbieGuide = (PlayerPrefs.GetInt("useGuide") == 1);
        flySkyHigh = (PlayerPrefs.GetInt("useQuickSky") == 1);
        fx = (PlayerPrefs.GetInt("useFX") == 1);
        sceneOfDusk = (PlayerPrefs.GetInt("duskScene") == 1);
        skillIndex = PlayerPrefs.GetInt("skillIndex");
        soulIndex = PlayerPrefs.GetInt("soulIndex");
    }

	public override void Exit()
	{
	}

    public override void OnGUI()
    {
        GUILayout.BeginVertical();

        bool temp_sceneOfDusk = GUILayout.Toggle(sceneOfDusk, "黄昏场景");
        bool temp_fx = GUILayout.Toggle(fx, "FX特效");
        bool temp_flySkyHigh = GUILayout.Toggle(flySkyHigh, "快速上天");
        bool temp_useNewbieGuide = GUILayout.Toggle(useNewbieGuide, "使用新手引导");

        string[] skillContext = new string[]{"无", "风", "火", "雷", "冰"};
        int temp_skillIndex = GUILayout.SelectionGrid(skillIndex, skillContext, 5);
        string[] soulContext = new string[] { "无", "红", "黄", "蓝", "紫" };
        int temp_soulIndex = GUILayout.SelectionGrid(soulIndex, soulContext, 5);

        if (temp_sceneOfDusk != sceneOfDusk)
        {
            sceneOfDusk = temp_sceneOfDusk;
            PlayerPrefs.SetInt("duskScene", sceneOfDusk ? 1 : 0);
        }
        if (temp_fx != fx)
        {
            fx = temp_fx;
            PlayerPrefs.SetInt("useFX", fx ? 1 : 0);
        }
        if (temp_flySkyHigh != flySkyHigh)
        {
            flySkyHigh = temp_flySkyHigh;
            PlayerPrefs.SetInt("useQuickSky", flySkyHigh ? 1 : 0);
        }
        if (temp_useNewbieGuide != useNewbieGuide)
        {
            useNewbieGuide = temp_useNewbieGuide;
            PlayerPrefs.SetInt("useGuide", useNewbieGuide ? 1 : 0);
        }
        if (temp_skillIndex != skillIndex)
        {
            skillIndex = temp_skillIndex;
            PlayerPrefs.SetInt("skillIndex", skillIndex);
        }
        if (temp_soulIndex != soulIndex)
        {
            soulIndex = temp_soulIndex;
            PlayerPrefs.SetInt("soulIndex", soulIndex);
        }
        GUILayout.EndVertical();
    }
}
