using UnityEngine;
using System.IO;
using System.Collections;

public class TestReadConfig : MonoBehaviour {

	void Start () {
		//ConfigReader.ReadConfigAsJson(typeof(Config), Path.Combine(ConfigExportPath.genJsonPath, "Config"));

		ConfigReader.ReadConfigAsBin(typeof(Config), "Config");
	}
}
