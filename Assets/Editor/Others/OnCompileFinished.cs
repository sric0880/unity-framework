using UnityEditor;

[InitializeOnLoad]
public class OnCompileFinished
{
	static OnCompileFinished()
	{
		EditorApplication.update = Update;
	}

	static void Update()
	{
		if (EditorApplication.isCompiling)
		{
			EditorApplication.isPlaying = false;
		}
	}
}
