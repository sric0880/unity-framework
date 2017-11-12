using UnityEngine;
using UF.Managers;

public class LoadTanks : MonoBehaviour
{
	public string sceneAssetBundle;
	public string sceneName;
	public string textAssetBundle;
	public string textAssetName;
	private string[] activeVariants;
	private bool bundlesLoaded;
	private bool sd, hd, normal, desert, english, danish;
	private string tankAlbedoStyle, tankAlbedoResolution, language;
	public bool async = false;

	void Awake ()
	{
		Log.Init(Log.Tag.Verbose, true, false);
		activeVariants = new string[2];
		bundlesLoaded = false;
	}

	// Creating the Temp UI for the demo in IMGui.
	void OnGUI ()
	{
		if (!bundlesLoaded)
		{
			// GUI Padding
			GUILayout.Space (20);
			GUILayout.BeginHorizontal ();
			GUILayout.Space (20);
			GUILayout.BeginVertical ();

			// GUI Buttons
			// New Line - Get HD/SD
			GUILayout.BeginHorizontal ();
			// Display the choice
			GUILayout.Toggle (sd, "");
			// Get player choice
			if (GUILayout.Button ("Load SD")) {sd = true; hd= false; tankAlbedoResolution = "sd";}
			// Display the choice
			GUILayout.Toggle (hd, "");
			// Get player choice
			if (GUILayout.Button ("Load HD")) {sd = false; hd = true; tankAlbedoResolution = "hd";}
			GUILayout.EndHorizontal ();
			
			// New Line - Get Normal/Desert
			GUILayout.BeginHorizontal ();
			// Display the choice
			GUILayout.Toggle (normal, "");
			// Get player choice
			if (GUILayout.Button ("Normal")) {normal = true; desert= false; tankAlbedoStyle = "normal";}
			// Display the choice
			GUILayout.Toggle (desert, "");
			// Get player choice
			if (GUILayout.Button ("Desert")) {normal = false; desert = true; tankAlbedoStyle = "desert";}
			GUILayout.EndHorizontal ();
			
			// New Line - Get Language
			GUILayout.BeginHorizontal ();
			// Display the choice
			GUILayout.Toggle (english, "");
			// Get player choice
			if (GUILayout.Button ("English")) {english = true; danish = false; language = "english";}
			// Display the choice
			GUILayout.Toggle (danish, "");
			// Get player choice
			if (GUILayout.Button ("Danish")) {english = false; danish = true; language = "danish";}
			GUILayout.EndHorizontal ();

			// GUI Padding
			GUILayout.Space (15);

			// Load the Scene
			if (GUILayout.Button ("Load Scene"))
			{
				// Remove the buttons
				bundlesLoaded = true;
				// Set the activeVariant
				activeVariants[0] = tankAlbedoStyle + "-" + tankAlbedoResolution;
				activeVariants[1] = language;
				// Show this in the log to make sure it is correct
				Debug.Log (activeVariants[0]);
				Debug.Log (activeVariants[1]);
				// Load the scene now!
				BeginExample();
			}

			// End GUI Padding
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
		// Show the two ActiveVariants in the console.
		Log.Debug(AssetBundleLoader.activeVariants [0]);
		Log.Debug(AssetBundleLoader.activeVariants [1]);

		if (async)
		{
			AssetBundleLoader.LoadManifestAsync(() =>
			{
				AssetBundleLoader.LoadSceneAsync(sceneAssetBundle, sceneName, true, null, null);
				ResourceManager.Instance.InstantiateGameObjectAsync(textAssetBundle, textAssetName, null, null);
			});
		}
		else
		{
			AssetBundleLoader.LoadManifest();
			// Load variant level which depends on variants.
			AssetBundleLoader.LoadScene(sceneAssetBundle, sceneName, true);
			// Load additonal assets, in this case a language specific banner
			ResourceManager.Instance.InstantiateGameObject(textAssetBundle, textAssetName);
		}

	}
}
