using UnityEngine;
using System.IO;
using System.Collections;

public class TestReadConfig : MonoBehaviour {

	void Start () {
		ConfigReader.ReadConfigAsBin(typeof(Config), "conf");
	}
}
