using System;
using System.Collections.Generic;
using UnityEngine;

namespace UF.Managers
{
	public class GameObjectDestroyer : MonoBehaviour
	{
		static Dictionary<string, int> counter;
		string assetName;

		public void AddRef(string assetName)
		{
			this.assetName = assetName;
			if (counter.ContainsKey(assetName))
			{
				++counter[assetName];
			}
			else
			{
				counter.Add(assetName, 1);
			}
		}

		void OnDestroy()
		{
			if (this.assetName != null)
			{
				int count;
				if (counter.TryGetValue(this.assetName, out count))
				{
					counter[this.assetName] = --count;
					if (count == 0)
					{
						ResourceManager.Instance.UnloadAsset(this.assetName);
					}
				}
			}
		}
	}

	// 缓存所有资源Object，不采用引用计数，外部负责清理缓存
	// 当AssetBundle对应的资源缓存清空时，AssetBundle的引用计数减一
	public class ResourceManager : MgrSingleton<ResourceManager>
	{
		Dictionary<string, UnityEngine.Object> cachedAssets = new Dictionary<string, UnityEngine.Object>();
		Dictionary<string, string> assetToBundleNameMap = new Dictionary<string, string>();

		/// <summary>
		/// 是否对实例化的GameObject进行引用计数
		/// 当实例化时，引用计数+1，销毁时引用技术-1，当==0时，清理缓存的GameObject
		/// 并且对应的AssetBundle引用计数-1
		/// </summary>
		public bool UseGameObjectReferenceCount = false;

		public override void OnInit()
		{
		}

		T LoadAsset<T>(string bundleName, string assetName) where T : UnityEngine.Object
		{
			if (cachedAssets.ContainsKey(assetName))
			{
				return cachedAssets[assetName] as T;
			}
			else
			{
				T __asset = AssetBundleLoader.LoadAsset<T>(bundleName, assetName);
				if (__asset != null)
				{
					cachedAssets[assetName] = __asset;
					assetToBundleNameMap[assetName] = bundleName;
					return __asset;
				}
			}
			return null;
		}

		void LoadAssetAsync<T>(string bundleName, string assetName, Action<T> onLoaded, Action<float> onProgress) where T : UnityEngine.Object
		{
			if (cachedAssets.ContainsKey(assetName))
			{
				if (onLoaded != null)
				{
					onLoaded(cachedAssets[assetName] as T);
				}
			}
			else
			{
				AssetBundleLoader.LoadAssetAsync<T>(bundleName, assetName, (asset) => {
					if (asset != null)
					{
						cachedAssets[assetName] = asset;
						assetToBundleNameMap[assetName] = bundleName;
					}
					if (onLoaded != null)
					{
						onLoaded(asset);
					}
				}, onProgress);
			}
		}

		public void UnloadAsset(string assetName)
		{
			UnityEngine.Object asset = null;
			string bundleName = null;
			if (!cachedAssets.TryGetValue(assetName, out asset) || !assetToBundleNameMap.TryGetValue(assetName, out bundleName))
			{
				Log.Error("[ResourceManager.UnloadAsset] {0} not loaded yet.", assetName);
			}
			if (asset is GameObject)
			{
				UnityEngine.Object.Destroy(asset);
			}
			else
			{
				Resources.UnloadAsset(asset);
			}
			cachedAssets.Remove(assetName);

			var loadedAb = AssetBundleLoader.GetLoadedAssetBundle(bundleName);
			if (loadedAb != null)
			{
				bool isUnloadBundle = true;
				var allAssetNames = loadedAb.assetBundle.GetAllAssetNames();
				for (int i = 0; i < allAssetNames.Length; ++i)
				{
					if (cachedAssets.ContainsKey(allAssetNames[i]))
					{
						isUnloadBundle = false;
						break;
					}
				}
				if (isUnloadBundle)
				{
					AssetBundleLoader.UnloadBundle(bundleName);
				}
			}
			else
			{
				Log.Error("[ResourceManager.UnloadAsset] assetbundle {0} has already unloaded.", bundleName);
			}
			assetToBundleNameMap.Remove(assetName);
		}

		public GameObject LoadPrefab(string bundleName, string assetName)
		{
			return LoadAsset<GameObject>(bundleName, assetName);
		}

		public void LoadPrefabAsync(string bundleName, string assetName, Action<GameObject> onLoaded, Action<float> onProgress)
		{
			LoadAssetAsync(bundleName, assetName, onLoaded, onProgress);
		}

		void AddGameObjectDestroyer(GameObject gameObj, string assetName)
		{
			if (UseGameObjectReferenceCount)
			{
				var destroyer = gameObj.AddComponent<GameObjectDestroyer>();
				destroyer.AddRef(assetName);
			}
		}

		public GameObject InstantiateGameObject(string bundleName, string assetName)
		{
			GameObject orgObj = LoadPrefab(bundleName, assetName);
			if (orgObj == null)
			{
				return null;
			}
			GameObject ret = UnityEngine.Object.Instantiate(orgObj) as GameObject;
			AddGameObjectDestroyer(ret, assetName);
			return ret;
		}

		public GameObject InstantiateGameObject(string bundleName, string assetName, Vector3 position, Quaternion rotation)
		{
			GameObject orgObj = LoadPrefab(bundleName, assetName);
			if (orgObj == null)
			{
				return null;
			}
			GameObject ret = UnityEngine.Object.Instantiate(orgObj) as GameObject;
			AddGameObjectDestroyer(ret, assetName);
			ret.transform.position = position;
			ret.transform.rotation = rotation;
			return ret;
		}

		public void InstantiateGameObjectAsync(string bundleName, string assetName, Action<GameObject> onLoaded, Action<float> onProgress)
		{
			LoadPrefabAsync(bundleName, assetName, orgObj => {
				if (orgObj != null)
				{
					GameObject ret = UnityEngine.Object.Instantiate(orgObj) as GameObject;
					AddGameObjectDestroyer(ret, assetName);
					if (onLoaded != null)
					{
						onLoaded(ret);
					}
				}
			}, onProgress);
		}

		public void InstantiateGameObjectAsync(string bundleName, string assetName, Vector3 position, Quaternion rotation, Action<GameObject> onLoaded, Action<float> onProgress)
		{
			LoadPrefabAsync(bundleName, assetName, orgObj => {
				if (orgObj != null)
				{
					GameObject ret = UnityEngine.Object.Instantiate(orgObj) as GameObject;
					AddGameObjectDestroyer(ret, assetName);
					ret.transform.position = position;
					ret.transform.rotation = rotation;
					if (onLoaded != null)
					{
						onLoaded(ret);
					}
				}
			}, onProgress);
		}

		public Texture LoadTexture(string bundleName, string assetName)
		{
			return LoadAsset<Texture>(bundleName, assetName);
		}

		public void LoadTextureAsync(string bundleName, string assetName, Action<Texture> onLoaded, Action<float> onProgress)
		{
			LoadAssetAsync(bundleName, assetName, onLoaded, onProgress);
		}

		public Material LoadMaterial(string bundleName, string assetName)
		{
			return LoadAsset<Material>(bundleName, assetName);
		}

		public void LoadMaterialAsync(string bundleName, string assetName, Action<Material> onLoaded, Action<float> onProgress)
		{
			LoadAssetAsync(bundleName, assetName, onLoaded, onProgress);
		}

		public Font LoadFont(string bundleName, string assetName)
		{
			return LoadAsset<Font>(bundleName, assetName);
		}

		public void LoadFontAsync(string bundleName, string assetName, Action<Font> onLoaded, Action<float> onProgress)
		{
			LoadAssetAsync(bundleName, assetName, onLoaded, onProgress);
		}

		public AudioClip LoadAudioClip(string bundleName, string assetName)
		{
			return LoadAsset<AudioClip>(bundleName, assetName);
		}

		public void LoadAudioClipAsync(string bundleName, string assetName, Action<AudioClip> onLoaded, Action<float> onProgress)
		{
			LoadAssetAsync(bundleName, assetName, onLoaded, onProgress);
		}

		public AnimationClip LoadAnimationClip(string bundleName, string assetName)
		{
			return LoadAsset<AnimationClip>(bundleName, assetName);
		}

		public void LoadAnimationClipAsync(string bundleName, string assetName, Action<AnimationClip> onLoaded, Action<float> onProgress)
		{
			LoadAssetAsync(bundleName, assetName, onLoaded, onProgress);
		}

		public RuntimeAnimatorController LoadAniController(string bundleName, string assetName)
		{
			return LoadAsset<RuntimeAnimatorController>(bundleName, assetName);
		}

		public void LoadAniControllerAsync(string bundleName, string assetName, Action<RuntimeAnimatorController> onLoaded, Action<float> onProgress)
		{
			LoadAssetAsync(bundleName, assetName, onLoaded, onProgress);
		}

		public Shader LoadShader(string bundleName, string assetName)
		{
			return LoadAsset<Shader>(bundleName, assetName);
		}

		public void LoadShaderAsync(string bundleName, string assetName, Action<Shader> onLoaded, Action<float> onProgress)
		{
			LoadAssetAsync(bundleName, assetName, onLoaded, onProgress);
		}

		public ShaderVariantCollection LoadShaderVarCollection(string bundleName, string assetName)
		{
			return LoadAsset<ShaderVariantCollection>(bundleName, assetName);
		}

		public void LoadShaderVarCollectionAsync(string bundleName, string assetName, Action<ShaderVariantCollection> onLoaded, Action<float> onProgress)
		{
			LoadAssetAsync(bundleName, assetName, onLoaded, onProgress);
		}

	}
}