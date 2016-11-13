//using UnityEngine;
//using UnityEditor;
//using System;
//using System.Text;
//using System.IO;
//using System.Collections.Generic;
//using System.Reflection;

//public class ConfigInspector : EditorWindow{

//	private Vector2 scrollPos;

//	/// <summary>
//	/// The static type to inspector
//	/// </summary>
//	private Type[] staticTypes = { typeof(Config)};
//	/// <summary>
//	/// Type name show in the window
//	/// </summary>
//	private string[] typeNames = {"Config"};
//	private List<int> changedIndex = new List<int>();
//	private bool changed
//	{
//		get { return changedIndex.Count > 0; }
//	}

//	private void OnEnable()
//	{
//		CreateFolderIfNeed();
//		readConfigFromJson();
//	}

//	private void CreateFolderIfNeed()
//	{
//		//if (!AssetDatabase.IsValidFolder(PathUtility.exportPath))
//		//{
//		//	AssetDatabase.CreateFolder("Assets", "[export]");
//		//}
//		//if (!AssetDatabase.IsValidFolder(PathUtility.genJsonPath))
//		//{
//		//	AssetDatabase.CreateFolder(PathUtility.exportPath, "jsons");
//		//}
//		//if (!AssetDatabase.IsValidFolder(PathUtility.genBinPath))
//		//{
//		//	AssetDatabase.CreateFolder(PathUtility.exportPath, "bin");
//		//}
//	}

//	private void OnGUI()
//	{
//		//static config inspector
//		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

//		for (int i = 0; i < staticTypes.Length; ++i)
//		{
//			if (printStatic(staticTypes[i], typeNames[i]))
//			{
//				changedIndex.Add(i);
//			}
//		}

//		EditorGUILayout.EndScrollView();
//		if (GUILayout.Button("Save"))
//		{
//			saveConfigAsJson();
//			saveConfigAsBin();
//		}
//	}

//	private void OnDestroy()
//	{
//		if (changed)
//		{
//			EditorUtility.DisplayDialog("Info", "Config has changed, all changes will be discarded.", "OK");
//		}
//	}

//	private bool printStatic(Type type, string name)
//	{
//		var exportFields = ClassFieldFilter.GetConfigFieldInfo(type);

//		if (exportFields.Count == 0)
//		{
//			return false;
//		}

//		foreach(var field in exportFields)
//		{
//			if (field.GetValue(null) == null)
//			{
//				var value = InstanceUtility.InstanceOfType(field.FieldType);
//				field.SetValue(null, value);
//			}
//		}

//		object NullObj = null;
//		return DataInspectorUtility.inspect(ref NullObj, type, name);
//	}

//	private void saveConfigAsBin()
//	{
//		CreateFolderIfNeed();
//		for (int i = 0; i < staticTypes.Length; ++i)
//		{
//			//ConfigWriter.WriteConfigAsBin(staticTypes[i], typeNames[i], PathUtility.genBinPath);
//		}
//		changedIndex.Clear();
//		DataInspectorUtility.clearChangedPath();
//	}

//	private void saveConfigAsJson()
//	{
//		CreateFolderIfNeed();
//		for (int i = 0; i < typeNames.Length; ++i)
//		{
//			var typeName = typeNames[i];
//			var folder = Path.Combine(PathUtility.genJsonPath, typeName);
//			if (!AssetDatabase.IsValidFolder(folder))
//			{
//				AssetDatabase.CreateFolder(PathUtility.genJsonPath, typeName);
//			}
//		}
//		for (int i = 0; i < staticTypes.Length; ++i)
//		{
//			var type = staticTypes[i];
//			var folder = Path.Combine(PathUtility.genJsonPath, typeNames[i]);
//			ConfigWriter.WriteConfigAsJson(type, folder);
//		}
//		changedIndex.Clear();
//		DataInspectorUtility.clearChangedPath();
//	}

//	private void readConfigFromJson()
//	{
//		for (int i = 0; i < staticTypes.Length; ++i)
//		{
//			var type = staticTypes[i];
//			var folder = Path.Combine(PathUtility.genJsonPath, typeNames[i]);
//			if (!AssetDatabase.IsValidFolder(folder))
//			{
//				continue;
//			}
//			ConfigReader.ReadConfigAsJson(type, folder);
//		}
//	}
//}
