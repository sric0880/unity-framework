using UnityEditor;
using System;
using System.Collections.Generic;

public class Build {

    private static readonly string[] targetScenes = {
		//TODO Add your own scene files
	};

    public static void BuildAndroidPlayer()
    {
        BuildPlayer(BuildTarget.Android);
    }

    public static void BuildiOSPlayer()
    {
        BuildPlayer(BuildTarget.iOS);
    }

    public static void BuildWindowsPlayer()
    {
        BuildPlayer(BuildTarget.StandaloneWindows);
    }

	private static void BuildPlayer(BuildTarget target)
	{
		Dictionary<string, string> args = GetCustomArguments();

		bool dev = args.ContainsKey("Dev") && args["Dev"] == "True";
        string targetPath = args.ContainsKey("Path") ? args["Path"] : null;
		if (string.IsNullOrEmpty(targetPath))
		{
			throw new Exception("Build Error: target path is null");
		}
        string error = BuildPipeline.BuildPlayer(targetScenes, targetPath, target, dev ? BuildOptions.Development : BuildOptions.None);
		if (!string.IsNullOrEmpty(error))
		{
			throw new Exception("Build Error: " + error);
		}
	}

    private static Dictionary<string, string> GetCustomArguments()
    {
        Dictionary<string, string> argsDict = new Dictionary<string, string>();
        string[] args = Environment.GetCommandLineArgs();
        string[] customArgs;
        string customArgStr = null;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i - 1] == "-CustomArgs")
            {
                customArgStr = args[i];
                break;
            }
        }

        if (customArgStr == null) return argsDict;

        customArgs = customArgStr.Split(';');

        foreach (string arg in customArgs)
        {
            string[] item = arg.Split('=');
            if (item.Length == 2)
            {
                argsDict.Add(item[0], item[1]);
            }
            else
            {
                UnityEngine.Debug.LogError("Invalid custom arguments");
            }
        }

        return argsDict;
    }
}
