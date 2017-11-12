using UnityEngine;
using UF.Managers;

public class LoadAssets : MonoBehaviour
{
	public string assetBundleName;
	public bool async = false;

	// Use this for initialization
	void Start ()
	{
		Log.Init(Log.Tag.Verbose, true, false);
		DontDestroyOnLoad(gameObject);

		if (async)
		{
			AssetBundleLoader.LoadManifestAsync(() => {
				ResourceManager.Instance.InstantiateGameObjectAsync(assetBundleName, assetBundleName, null, null);
			});
		}
		else
		{
			AssetBundleLoader.LoadManifest();
			ResourceManager.Instance.InstantiateGameObject(assetBundleName, assetBundleName);
		}
	}
}
