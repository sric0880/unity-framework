using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

public class ConfigEditor : EditorWindow{

	private static readonly string _autoGenPath = "Assets/Scripts/Configuration/AutoGen";
	private Vector2 scrollPos;

	/// <summary>
	/// The static type to inspector
	/// </summary>
	private Type[] staticTypes = { typeof(Config)};
	/// <summary>
	/// Type name show in the window
	/// </summary>
	private string[] typeNames = {"Config"};
	private List<int> changedIndex = new List<int>();
	private bool changed
	{
		get { return changedIndex.Count > 0; }
	}

	private void OnEnable()
	{
		CreateFolderIfNeed();
		readConfigFromJson();
	}

	private void CreateFolderIfNeed()
	{
		if (!AssetDatabase.IsValidFolder(ConfigExportPath.exportPath))
		{
			AssetDatabase.CreateFolder("Assets", "[export]");
		}
		if (!AssetDatabase.IsValidFolder(ConfigExportPath.genJsonPath))
		{
			AssetDatabase.CreateFolder(ConfigExportPath.exportPath, "jsons");
		}
		if (!AssetDatabase.IsValidFolder(ConfigExportPath.genBinPath))
		{
			AssetDatabase.CreateFolder(ConfigExportPath.exportPath, "bin");
		}
	}

	private void OnGUI()
	{
		//static config inspector
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		for (int i = 0; i < staticTypes.Length; ++i)
		{
			if (printStatic(staticTypes[i], typeNames[i]))
			{
				changedIndex.Add(i);
			}
		}

		EditorGUILayout.EndScrollView();
		if (GUILayout.Button("Save"))
		{
			saveConfigAsJson();
			saveConfigAsBin();
		}
		if (GUILayout.Button("Gen binary reader & writer"))
		{
			genBinSerializationCode();
		}
	}

	private void OnDestroy()
	{
		if (changed)
		{
			if (EditorUtility.DisplayDialog("Info", "Config has changed, save it first.", "Save"))
			{
				saveConfigAsJson();
			}
		}
	}

	private bool printStatic(Type type, string name)
	{
		var exportFields = ClassFieldFilter.GetConfigFieldInfo(type);

		if (exportFields.Count == 0)
		{
			return false;
		}

		foreach(var field in exportFields)
		{
			if (field.GetValue(null) == null)
			{
				var value = InstanceUtility.InstanceOfType(field.FieldType);
				field.SetValue(null, value);
			}
		}

		object NullObj = null;
		return DataInspectorUtility.inspect(ref NullObj, type, name);
	}

	private void saveConfigAsBin()
	{
		CreateFolderIfNeed();
		for (int i = 0; i < staticTypes.Length; ++i)
		{
			ConfigWriter.WriteConfigAsBin(staticTypes[i], typeNames[i], ConfigExportPath.genBinPath);
		}
		changedIndex.Clear();
		DataInspectorUtility.clearChangedPath();
	}

	private void saveConfigAsJson()
	{
		CreateFolderIfNeed();
		for (int i = 0; i < typeNames.Length; ++i)
		{
			var typeName = typeNames[i];
			var folder = Path.Combine(ConfigExportPath.genJsonPath, typeName);
			if (!AssetDatabase.IsValidFolder(folder))
			{
				AssetDatabase.CreateFolder(ConfigExportPath.genJsonPath, typeName);
			}
		}
		for (int i = 0; i < staticTypes.Length; ++i)
		{
			var type = staticTypes[i];
			var folder = Path.Combine(ConfigExportPath.genJsonPath, typeNames[i]);
			ConfigWriter.WriteConfigAsJson(type, folder);
		}
		changedIndex.Clear();
		DataInspectorUtility.clearChangedPath();
	}

	private void readConfigFromJson()
	{
		for (int i = 0; i < staticTypes.Length; ++i)
		{
			var type = staticTypes[i];
			var folder = Path.Combine(ConfigExportPath.genJsonPath, typeNames[i]);
			if (!AssetDatabase.IsValidFolder(folder))
			{
				continue;
			}
			ConfigReader.ReadConfigAsJson(type, folder);
		}
	}

	private void genBinSerializationCode()
	{
		//Clear AutoGen folder
		if (Directory.Exists(_autoGenPath))
		{
			Directory.Delete(_autoGenPath, true);
		}
		BinarySerializerCodeGenerator gen = new BinarySerializerCodeGenerator();
		for (int i = 0; i < staticTypes.Length; ++i)
		{
			gen.Register(staticTypes[i]);
		}
		gen.GenCode(folder:_autoGenPath);
//		gen.Register(typeof (GameData));
//		gen.ExportGeneratedFiles(confgenFolder);
//		
//		ConfigInitCodeGen cgen = new ConfigInitCodeGen();
//		cgen.ExportGeneratedFiles(confgenFolder);
	}
}
