using UnityEngine;

public class LoadScenes : MonoBehaviour
{
	public string sceneBundleName;
	public string sceneName;
	public bool addictive;
	public bool async = false;
	
	// Use this for initialization
	void Start ()
	{
		Log.Init(Log.Tag.Verbose, true, false);
		DontDestroyOnLoad(gameObject);

		if (async)
		{
			AssetBundleLoader.LoadManifestAsync(() =>
			{
				AssetBundleLoader.LoadSceneAsync(sceneBundleName, sceneName, addictive, null, null);
			});
		}
		else
		{
			AssetBundleLoader.LoadManifest();
			AssetBundleLoader.LoadScene(sceneBundleName, sceneName, addictive);
		}

	}
}
