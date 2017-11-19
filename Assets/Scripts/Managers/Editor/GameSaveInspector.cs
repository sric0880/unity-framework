using UnityEngine;
using UnityEditor;

public class GameSaveInspector : EditorWindow
{
    [MenuItem("Window/Inspector/Game Save")]
	private static void OpenConfigEditor()
	{
		GameSaveInspector window = (GameSaveInspector)EditorWindow.GetWindow(typeof(GameSaveInspector));
		window.Show();
	}

	private Vector2 scrollPos;

	private void OnGUI()
	{
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		object obj = UF.Managers.GameSaveManager.Instance;
		DataInspectorUtility.inspect(ref obj, typeof(UF.Managers.GameSaveManager), "GameSave");
		EditorGUILayout.EndScrollView();
	}
}
