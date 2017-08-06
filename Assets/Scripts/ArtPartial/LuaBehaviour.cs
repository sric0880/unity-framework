using UnityEngine;

namespace LuaFramework
{
	[System.Serializable]
	public class Injection
	{
		public string name;
		public GameObject gameObject;
	}

	public partial class LuaBehaviour : MonoBehaviour
	{
		public bool hasStartFunc;
		public bool hasEnableFunc;
		public Injection[] injections;
	}
}