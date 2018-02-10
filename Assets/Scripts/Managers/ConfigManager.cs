using System;
using System.IO;
using UnityEngine;
using UF.Config;

namespace UF.Managers
{
	/// <summary>
	/// Config manager.
	/// </summary>
	public class ConfigManager : MgrSingleton<ConfigManager>
	{
		public Main config;
		public Launch launch;

		public override void OnInit()
		{
		}

		public void LoadLaunch()
		{
			launch = Load<Launch>();
		}

		public void LoadMain()
		{
			config = Load<Main>();
		}

		private T Load<T>() where T : ISerializable, new()
		{
			string path = Path.Combine (FileUtils.binary_config_folder, ConfigHelper.GetExportDirName<T>() + ".bin");
			var stream = FileUtils.GetMemoryStreamFromFile (path);
			return ConfigHelper.ReadConfigAsBin<T>(stream);
		}

		private void LoadAsync<T>(Action<T> finished) where T : ISerializable, new()
		{
			string path = Path.Combine (FileUtils.binary_config_folder, ConfigHelper.GetExportDirName<T>() + ".bin");
			FileUtils.GetMemoryStreamFromFileAsync (path, (st) => {
				var c = ConfigHelper.ReadConfigAsBin<T>(st);
				if (finished != null) {
					finished(c);
				}
			});
		}

	}
}