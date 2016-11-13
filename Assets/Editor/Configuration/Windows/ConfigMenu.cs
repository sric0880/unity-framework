//using UnityEditor;
//using System.IO;

//public class ConfigMenu {

//	[MenuItem("Config/Inspector")]
//	private static void OpenConfigEditor()
//	{
//		ConfigInspector window = (ConfigInspector) EditorWindow.GetWindow(typeof(ConfigInspector));
//		window.Show();
//	}

//	private static readonly string _autoGenPath = "Assets/Scripts/Configuration/AutoGen";
//	[MenuItem("Config/CodeGen And JsonExample")]
//	private static void CodeGen()
//	{
//		//Clear AutoGen folder
//		if (Directory.Exists(_autoGenPath))
//		{
//			Directory.Delete(_autoGenPath, true);
//		}
//		BinarySerializerCodeGenerator gen = new BinarySerializerCodeGenerator();
//		for (int i = 0; i < staticTypes.Length; ++i)
//		{
//			gen.Register(staticTypes[i]);
//		}
//		gen.GenCode(folder: _autoGenPath);
//	}
//}
