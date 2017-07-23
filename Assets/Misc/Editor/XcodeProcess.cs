using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEditor.Callbacks;

public class XcodeProcess : MonoBehaviour {
	
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
	{
		if (buildTarget == BuildTarget.iOS)
		{
			string projPath = PBXProject.GetPBXProjectPath(path);
			PBXProject proj = new PBXProject();

			proj.ReadFromString(File.ReadAllText(projPath));
			string target = proj.TargetGuidByName("Unity-iPhone");

			//proj.AddFrameworkToProject(target, "AssetsLibrary.framework", false);

			//proj.AddFileToBuild(target, proj.AddFile("Frameworks/mylib.framework", "Frameworks/mylib.framework", PBXSourceTree.Source));

			proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
			//proj.SetBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
			//proj.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Frameworks");

			File.WriteAllText(projPath, proj.WriteToString());
		}
	}
}
