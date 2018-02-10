using System.IO;

namespace UF.Config
{
	public interface ISerializable
	{
		void Serialize(BinaryWriter o);
		void Deserialize(BinaryReader o);
	}
}
