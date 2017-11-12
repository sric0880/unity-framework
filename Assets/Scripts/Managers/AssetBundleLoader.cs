//#define PROFILE
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

// AssetBundle可以包含多个Assets
public class LoadedAssetBundle
{
	public AssetBundle assetBundle;
	public int referenceCount;

	public LoadedAssetBundle(AssetBundle assetBundle)
	{
		this.assetBundle = assetBundle;
		this.referenceCount = 1;
	}
}

// 所有加载的ab包做缓存
// 从ab包中加载的资源，使用缓存策略
public static class AssetBundleLoader
{
	public static string[] activeVariants = { };
	static AssetBundleManifest assetBundleManifest = null;
	public static Dictionary<string, LoadedAssetBundle> loadedAssetBundles = new Dictionary<string, LoadedAssetBundle>();
	static Dictionary<string, IObservable<AssetBundleCreateRequest> > loadingAssetBundles = new Dictionary<string, IObservable<AssetBundleCreateRequest> >();
	public static Dictionary<string, string[]> dependencies = new Dictionary<string, string[]>();
	private static readonly string manifestAssetName = "AssetBundleManifest";

	//同步加载依赖文件
	public static void LoadManifest()
	{
		assetBundleManifest = LoadAsset<AssetBundleManifest>(PlatformNameUtils.GetPlatformName(), manifestAssetName);
	}

	//异步加载依赖文件
	public static void LoadManifestAsync(Action onLoaded = null)
	{
		LoadAssetAsync<AssetBundleManifest>(PlatformNameUtils.GetPlatformName(), manifestAssetName, manifest =>
		{
			assetBundleManifest = manifest;
			if (onLoaded != null) onLoaded();
		}, null);
	}

	//同步加载资源
	public static T LoadAsset<T>(string assetBundleName, string assetName) where T : UnityEngine.Object
	{
#if PROFILE
		float startTime = Time.realtimeSinceStartup;
#endif
		var bundle = __LoadBundle(assetBundleName, manifestAssetName == assetName);
		if (bundle != null)
		{
#if PROFILE
			var asset = bundle.assetBundle.LoadAsset<T>(assetName);
			float elapsedTime = Time.realtimeSinceStartup - startTime;
			Log.Profile("asset {0} load in {1} ms", assetName, elapsedTime * 1000);
			return asset;
#else
			return bundle.assetBundle.LoadAsset<T>(assetName);
#endif
		}
		return null;
	}

	//同步加载资源
	public static T[] LoadAllAssets<T>(string assetBundleName) where T : UnityEngine.Object
	{
#if PROFILE
		float startTime = Time.realtimeSinceStartup;
#endif
		var bundle = __LoadBundle(assetBundleName, false);
		if (bundle != null)
		{
#if PROFILE
			var allAssets = bundle.assetBundle.LoadAllAssets<T>();
			float elapsedTime = Time.realtimeSinceStartup - startTime;
			Log.Profile("all assets in bundle {0} load in {1} ms", assetBundleName, elapsedTime * 1000);
			return allAssets;
#else
			return bundle.assetBundle.LoadAllAssets<T>();
#endif

		}
		return null;
	}

	//同步加载场景，底层使用LoadFromFile
	public static void LoadScene(string assetBundleName, string sceneName, bool addictive)
	{
#if PROFILE
		float startTime = Time.realtimeSinceStartup;
#endif
		var scene = SceneManager.GetSceneByName(sceneName);
		if (scene.isLoaded)
		{
			Log.Error("Scene {0} already loaded", sceneName);
			return;
		}
		var bundle = __LoadBundle(assetBundleName, false);
		if (bundle != null)
		{
			if (addictive)
			{
				SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
			}
			else
			{
				SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
			}
		}
#if PROFILE
		float elapsedTime = Time.realtimeSinceStartup - startTime;
		Log.Profile("scene {0} load in {1} ms", sceneName, elapsedTime * 1000);
#endif
	}

	public static LoadedAssetBundle GetLoadedAssetBundle(string assetBundleName)
	{
		assetBundleName = __RemapVariantName(assetBundleName);
		LoadedAssetBundle bundle = null;
		loadedAssetBundles.TryGetValue(assetBundleName, out bundle);
		return bundle;
	}

	//异步加载资源，底层使用LoadFromFile
	public static void LoadAssetAsync<T>(string assetBundleName, string assetName, Action<T> onLoaded, Action<float> onProgress) where T : UnityEngine.Object
	{
		__LoadBundleAsync(assetBundleName, loadedAb => __LoadAssetAsync(loadedAb, assetName, x => { if (onLoaded != null) onLoaded(x.asset as T); }, onProgress), manifestAssetName == assetName);
	}

	//异步加载资源，底层使用LoadFromFile
	public static void LoadAllAssetAsync<T>(string assetBundleName, Action<T[]> onLoaded, Action<float> onProgress) where T : UnityEngine.Object
	{
		__LoadBundleAsync(assetBundleName, loadedAb => __LoadAllAssetsAsync(loadedAb, x => { if (onLoaded != null) onLoaded(x.allAssets as T[]); }, onProgress), false);
	}

	//异步加载场景，底层使用LoadFromFile
	public static void LoadSceneAsync(string assetBundleName, string sceneName, bool addictive, Action onLoaded, Action<float> onProgress)
	{
		var scene = SceneManager.GetSceneByName(sceneName);
		if (scene.isLoaded)
		{
			Log.Error("Scene {0} already loaded", sceneName);
			return;
		}
		__LoadBundleAsync(assetBundleName, loadedAb => __LoadSceneAsync(sceneName, addictive, onLoaded, onProgress), false);
	}

	static void __LoadAssetAsync(LoadedAssetBundle loadedAb, string assetName, Action<AssetBundleRequest> onLoaded, Action<float> onProgress)
	{
#if PROFILE
		float startTime = Time.realtimeSinceStartup;
#endif
		var request = loadedAb.assetBundle.LoadAssetAsync(assetName);
		request.AsAsyncOperationObservable()
			   .Do(x =>
		{
			if (onProgress != null)
			{
				onProgress(x.progress);
			}
		}).Last()
#if PROFILE
		.Subscribe((req) =>
		{
			float elapsedTime = Time.realtimeSinceStartup - startTime;
			Log.Profile("asset {0} async load in {1} ms", assetName, elapsedTime * 1000);
			if (onLoaded != null) onLoaded(req);
		});
#else
	   .Subscribe(onLoaded);
#endif
	}

	static void __LoadAllAssetsAsync(LoadedAssetBundle loadedAb, Action<AssetBundleRequest> onLoaded, Action<float> onProgress)
	{
#if PROFILE
		float startTime = Time.realtimeSinceStartup;
#endif
		var request = loadedAb.assetBundle.LoadAllAssetsAsync();
		request.AsAsyncOperationObservable()
			   .Do(x =>
		{
			if (onProgress != null)
			{
				onProgress(x.progress);
			}
		}).Last()
#if PROFILE
		.Subscribe((req) => {
			float elapsedTime = Time.realtimeSinceStartup - startTime;
			Log.Profile("all assets in bundle {0} async load in {1} ms", loadedAb.assetBundle.name, elapsedTime * 1000);
			if (onLoaded != null) onLoaded(req);
		});
#else
	   .Subscribe(onLoaded);
#endif
	}

	static void __LoadSceneAsync(string sceneName, bool addictive, Action onLoad, Action<float> onProgress)
	{
#if PROFILE
		float startTime = Time.realtimeSinceStartup;
#endif
		AsyncOperation request;
		if (addictive)
		{
			request = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		}
		else
		{
			request = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		}
		request.AsObservable().Do(x =>
		{
			if (onProgress != null)
			{
				onProgress(x.progress);
			}
#if PROFILE
		}, () => {
			float elapsedTime = Time.realtimeSinceStartup - startTime;
			Log.Profile("scene {0} async load in {1} ms", sceneName, elapsedTime * 1000);
			if (onLoad != null) onLoad();
		}).Subscribe();
#else
		}, onLoad == null ? () => { } : onLoad).Subscribe();
#endif
	}

	// Remaps the asset bundle name to the best fitting asset bundle variant.
	static string __RemapVariantName(string assetBundleName)
	{
		string[] bundlesWithVariant = assetBundleManifest.GetAllAssetBundlesWithVariant();
		int dotIndex = assetBundleName.IndexOf('.');
		if (dotIndex != -1)
		{
			assetBundleName = assetBundleName.Substring(0, dotIndex);
		}

		int bestFit = int.MaxValue;
		int bestFitIndex = -1;
		// Loop all the assetBundles with variant to find the best fit variant assetBundle.
		for (int i = 0; i < bundlesWithVariant.Length; ++i)
		{
			string[] curSplit = bundlesWithVariant[i].Split('.');
			if (curSplit[0] != assetBundleName)
				continue;

			int found = System.Array.IndexOf(activeVariants, curSplit[1]);

			// If there is no active variant found. We still want to use the first 
			if (found == -1)
				found = int.MaxValue - 1;

			if (found < bestFit)
			{
				bestFit = found;
				bestFitIndex = i;
			}
		}

		if (bestFit == int.MaxValue - 1)
		{
			Log.Warning("Ambigious asset bundle variant chosen because there was no matching active variant: ", bundlesWithVariant[bestFitIndex]);
		}

		if (bestFitIndex != -1)
		{
			return bundlesWithVariant[bestFitIndex];
		}
		else
		{
			return assetBundleName;
		}
	}

	static string[] __GetDependencies(string assetBundleName)
	{
		// Get dependecies from the AssetBundleManifest object..
		string[] depends = assetBundleManifest.GetAllDependencies(assetBundleName);
		if (depends.Length > 0)
		{
			for (int i = 0; i < depends.Length; i++)
			{
				depends[i] = __RemapVariantName(depends[i]);
			}

			// Record and load all dependencies.
			dependencies.Add(assetBundleName, depends);
			return depends;
		}
		return null;
	}

	// 同步方法，支持非压缩和LZ4
	// 加载LZMA会先解压到内存
	// unity5.4支持从jar包中直接读取StreamingAssets
	static LoadedAssetBundle __LoadBundle__(string bundleName)
	{
		LoadedAssetBundle bundle = null;
		loadedAssetBundles.TryGetValue(bundleName, out bundle);
		if (bundle == null)
		{
			var relativePath = Path.Combine(FileUtils.bundle_folder, bundleName);
			var fullPath = FileUtils.GetFullPathForFilename(relativePath);
			if (fullPath != null)
			{
				var ab = AssetBundle.LoadFromFile(fullPath);
				if (ab == null)
				{
					Log.Error("[LoadFromFile] Failed to load assetBundle at {0}", bundleName);
					return null;
				}
				bundle = new LoadedAssetBundle(ab);
				loadedAssetBundles.Add(bundleName, bundle);
			}
		}
		else
		{
			++bundle.referenceCount;
		}
		return bundle;
	}

	static LoadedAssetBundle __LoadBundle(string assetBundleName, bool isManifest)
	{
#if PROFILE
		float startTime = Time.realtimeSinceStartup;
#endif
		if (!isManifest)
		{
			if (assetBundleManifest == null)
			{
				Log.Error("Please initialize AssetBundleManifest by calling AssetBundleLoader.LoadManifest or LoadManifestAsync");
				return null;
			}
			assetBundleName = __RemapVariantName(assetBundleName);
			var depends = __GetDependencies(assetBundleName);
			if (depends != null)
			{
				for (int i = 0; i < depends.Length; ++i)
				{
					__LoadBundle__(depends[i]);
				}
			}
		}
		LoadedAssetBundle mainAssetBundle = __LoadBundle__(assetBundleName);

#if PROFILE
		float elapsedTime = Time.realtimeSinceStartup - startTime;
		Log.Profile("assetbundle {0} load in {1} ms", assetBundleName, elapsedTime * 1000);
#endif
		return mainAssetBundle;
	}

	static LoadedAssetBundle __LoadBundleAsync__(string bundleName, List<IObservable<AssetBundleCreateRequest>> toLoadList, List<string> toLoadBundleNames)
	{
		LoadedAssetBundle bundle = null;
		loadedAssetBundles.TryGetValue(bundleName, out bundle);
		if (bundle == null)
		{
			if (loadingAssetBundles.ContainsKey(bundleName))
			{
				toLoadList.Add(loadingAssetBundles[bundleName]);
				toLoadBundleNames.Add(bundleName);
			}
			else
			{
				var relativePath = Path.Combine(FileUtils.bundle_folder, bundleName);
				var fullPath = FileUtils.GetFullPathForFilename(relativePath);
				if (fullPath != null)
				{
					var request = AssetBundle.LoadFromFileAsync(fullPath);
					var loadObservable = request.AsAsyncOperationObservable();
					loadingAssetBundles.Add(bundleName, loadObservable);
					toLoadList.Add(loadObservable);
					toLoadBundleNames.Add(bundleName);
				}
			}
			return null;
		}
		else
		{
			++bundle.referenceCount;
			return bundle;
		}
	}

	//异步加载AssetBundle
	static void __LoadBundleAsync(string assetBundleName, Action<LoadedAssetBundle> onLoaded, bool isManifest)
	{
#if PROFILE
		float startTime = Time.realtimeSinceStartup;
#endif
		string[] depends = null;
		if (!isManifest)
		{
			if (assetBundleManifest == null)
			{
				Log.Error("Please initialize AssetBundleManifest by calling AssetBundleLoader.LoadManifest or LoadManifestAsync");
				return;
			}
			assetBundleName = __RemapVariantName(assetBundleName);
			depends = __GetDependencies(assetBundleName);
		}
		int capacity = depends == null ? 1 : depends.Length + 1;
		List<IObservable<AssetBundleCreateRequest>> toLoadList = new List<IObservable<AssetBundleCreateRequest>>(capacity);
		List<string> toLoadBundleNames = new List<string>(capacity);
		if (depends != null)
		{
			for (int i = 0; i < depends.Length; ++i)
			{
				__LoadBundleAsync__(depends[i], toLoadList, toLoadBundleNames);
			}
		}
		LoadedAssetBundle mainBundle = __LoadBundleAsync__(assetBundleName, toLoadList, toLoadBundleNames);

		if (toLoadList.Count == 0)
		{
			if (onLoaded != null)
			{
				onLoaded(mainBundle);
			}
		}
		else
		{ 
			Observable.WhenAll(toLoadList)
				  .DoOnError(exp =>
			{
				Log.Error(exp.Message);
				Log.Error(exp.StackTrace);
				for (int i = 0; i < toLoadBundleNames.Count; ++i)
				{
					loadingAssetBundles.Remove(toLoadBundleNames[i]);
				}
			})
					  .Last()
					  .Subscribe(toloadlist =>
			{
				for (int i = 0; i < toloadlist.Length; ++i)
				{
					var bundleName = toLoadBundleNames[i];
					if (!loadedAssetBundles.ContainsKey(bundleName))
					{
						loadedAssetBundles.Add(bundleName, new LoadedAssetBundle(toloadlist[i].assetBundle));
					}
					loadingAssetBundles.Remove(bundleName);
				}
				LoadedAssetBundle loadedAb = null;
				if (mainBundle != null)
				{
					loadedAb = mainBundle;
				}
				else
				{
					loadedAssetBundles.TryGetValue(assetBundleName, out loadedAb);
					if (loadedAb == null)
					{
						Log.Error("assetbundle {0} not loaded when it ought to", assetBundleName);
					}
				}
				if (onLoaded != null)
				{
					onLoaded(loadedAb);
				}
#if PROFILE
				float elapsedTime = Time.realtimeSinceStartup - startTime;
				Log.Profile("assetbundle {0} async load in {1} ms", assetBundleName, elapsedTime * 1000);
#endif
			});
		}
	}

	public static void UnloadBundle(string assetBundleName)
	{
		assetBundleName = __RemapVariantName(assetBundleName);
		__UnloadBundle(assetBundleName);
		__UnloadDependencies(assetBundleName);
	}

	static void __UnloadBundle(string assetBundleName)
	{
		LoadedAssetBundle bundle = null;
		loadedAssetBundles.TryGetValue(assetBundleName, out bundle);
		if (bundle == null)
		{
			Log.Error("unload assetbundle {0}, but it has not been loaded yet", assetBundleName);
			return;
		}

		if (--bundle.referenceCount == 0)
		{
			bundle.assetBundle.Unload(true);
			loadedAssetBundles.Remove(assetBundleName);

			Log.Info("{0} has been unloaded successfully", assetBundleName);
		}
	}

	static void __UnloadDependencies(string assetBundleName)
	{
		string[] depends = null;
		if (!dependencies.TryGetValue(assetBundleName, out depends))
		{
			return;
		}

		// Loop dependencies.
		foreach (var depend in depends)
		{
			__UnloadBundle(depend);
		}

		dependencies.Remove(assetBundleName);
	}
}
