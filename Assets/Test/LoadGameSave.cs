using UnityEngine;

public class LoadGameSave : MonoBehaviour
{
	void Start()
	{
		Log.Init(Log.Tag.Verbose, true, false);
		UF.Managers.GameSaveManager.Instance.OnInit();
		UF.Managers.GameSaveManager.Instance.Load();
		var monster = UF.Managers.GameSaveManager.Instance.monster;

		UF.Managers.GameSaveManager.Instance.Save();
	}
}
