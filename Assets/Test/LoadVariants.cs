using UnityEngine;
using UF.Managers;

public class LoadVariants : MonoBehaviour
{
	const string variantSceneBundle = "variantscene";
	const string variantSceneName = "VariantScene";
	private string[] activeVariants;
	private bool bundlesLoaded;				// used to remove the loading buttons
	public bool addictive;
	public bool async = false;

	void Awake ()
	{
		Log.Init(Log.Tag.Verbose, true, false);
		activeVariants = new string[1];
		bundlesLoaded = false;
	}

	void OnGUI ()
	{
		if (!bundlesLoaded)
		{
			GUILayout.Space (20);
			GUILayout.BeginHorizontal ();
			GUILayout.Space (20);
			GUILayout.BeginVertical ();
			if (GUILayout.Button ("Load SD"))
			{
				activeVariants[0] = "sd";
				bundlesLoaded = true;
				BeginExample();
			}
			GUILayout.Space (5);
			if (GUILayout.Button ("Load HD"))
			{
				activeVariants[0] = "hd";
				bundlesLoaded = true;
				BeginExample();
			}
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();
		}
	}
	
	// Use this for initialization
	void BeginExample ()
	{
		DontDestroyOnLoad(gameObject);

		// Set active variants.
		AssetBundleLoader.activeVariants = activeVariants;

		if (async)
		{
			AssetBundleLoader.LoadManifestAsync(() =>
			{
				AssetBundleLoader.LoadSceneAsync(variantSceneBundle, variantSceneName, addictive, null, null);
			});
		}
		else
		{
			AssetBundleLoader.LoadManifest();
			AssetBundleLoader.LoadScene(variantSceneBundle, variantSceneName, addictive);
		}
	}
}
