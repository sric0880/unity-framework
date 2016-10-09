using UnityEditor;

public class ConfigMenu {

	[MenuItem("Config/Open Editor")]
	private static void OpenEditor()
	{
		ConfigEditor window = (ConfigEditor) EditorWindow.GetWindow(typeof(ConfigEditor));
		window.Show();
	}
}
