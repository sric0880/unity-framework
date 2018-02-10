using UnityEngine;
using System.IO;
using System.Collections;

public class TestReadConfig : MonoBehaviour {

	void Start () {
		Log.Init(Log.Tag.Verbose, true, false);
		UF.Managers.ConfigManager.Instance.OnInit();
		UF.Managers.ConfigManager.Instance.LoadLaunch();
		UF.Managers.ConfigManager.Instance.LoadMain();
	}
}
