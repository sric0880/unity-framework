using System.Collections;
using UnityEngine;
using UF.GameSave;
using FlatBuffers;

namespace UF.Managers
{
	/// <summary>
	/// GameSave manager.
	/// </summary>
	public class GameSaveManager : MgrSingleton<GameSaveManager>
	{
		public Monster monster;

		public override void OnInit()
		{
		}

		public void Load()
		{
			string gamesaveFilePath = FileUtils.GetWritablePathForPathname(FileUtils.gamesave(11111));
			byte[] bytes = FileUtils.GetBytesFromFile(gamesaveFilePath);
			if (bytes == null)
			{
				// Default value
				var builder = new FlatBufferBuilder(1);
				Monster.StartMonster(builder);
				var orc = Monster.EndMonster(builder);
				Monster.FinishMonsterBuffer(builder, orc);
				monster = Monster.GetRootAsMonster(builder.DataBuffer);
			}
			else
			{
				monster = Monster.GetRootAsMonster(new ByteBuffer(bytes));
			}
		}

		public void Save()
		{
			byte[] bytes = monster.ByteBuffer.Data;
			FileUtils.WriteToFile(FileUtils.gamesave(11111), bytes);
		}

	}
}