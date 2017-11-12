using UnityEngine;
using UnityEditor;

public class ResourceInspector : EditorWindow {

	[MenuItem("Window/Inspector/Resources")]
	private static void OpenConfigEditor()
	{
		ResourceInspector window = (ResourceInspector)EditorWindow.GetWindow(typeof(ResourceInspector));
		window.Show();
	}
	
	private Vector2 scrollPos;

	private void OnGUI()
	{
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		object NullObj = null;
		DataInspectorUtility.inspect(ref NullObj, typeof(AssetBundleLoader), "Resources");
		EditorGUILayout.EndScrollView();
	}
}
