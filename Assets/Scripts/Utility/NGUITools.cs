using System;
using UnityEngine;

public static class NGUITools
{
	/// <summary>
	/// 搜索子物体组件-GameObject版
	/// </summary>
	public static T Get<T>(GameObject go, string subnode) where T : Component
	{
		if (go != null)
		{
			Transform sub = go.transform.FindChild(subnode);
			if (sub != null) return sub.GetComponent<T>();
		}
		return null;
	}

	/// <summary>
	/// 搜索子物体组件-Transform版
	/// </summary>
	public static T Get<T>(Transform go, string subnode) where T : Component
	{
		if (go != null)
		{
			Transform sub = go.FindChild(subnode);
			if (sub != null) return sub.GetComponent<T>();
		}
		return null;
	}

	/// <summary>
	/// 搜索子物体组件-Component版
	/// </summary>
	public static T Get<T>(Component go, string subnode) where T : Component
	{
		return go.transform.FindChild(subnode).GetComponent<T>();
	}

	/// <summary>
	/// 添加组件
	/// </summary>
	public static T Add<T>(GameObject go) where T : Component
	{
		if (go != null)
		{
			T[] ts = go.GetComponents<T>();
			for (int i = 0; i < ts.Length; i++)
			{
				if (ts[i] != null) GameObject.Destroy(ts[i]);
			}
			return go.gameObject.AddComponent<T>();
		}
		return null;
	}

	/// <summary>
	/// 添加组件
	/// </summary>
	public static T Add<T>(Transform go) where T : Component
	{
		return Add<T>(go.gameObject);
	}

	/// <summary>
	/// 查找子对象
	/// </summary>
	public static GameObject Child(GameObject go, string subnode)
	{
		return Child(go.transform, subnode);
	}

	/// <summary>
	/// 查找子对象
	/// </summary>
	public static GameObject Child(Transform go, string subnode)
	{
		Transform tran = go.FindChild(subnode);
		if (tran == null) return null;
		return tran.gameObject;
	}

	/// <summary>
	/// 取平级对象
	/// </summary>
	public static GameObject Peer(GameObject go, string subnode)
	{
		return Peer(go.transform, subnode);
	}

	/// <summary>
	/// 取平级对象
	/// </summary>
	public static GameObject Peer(Transform go, string subnode)
	{
		Transform tran = go.parent.FindChild(subnode);
		if (tran == null) return null;
		return tran.gameObject;
	}

	/// <summary>
	/// 清除所有子节点
	/// </summary>
	public static void ClearChild(Transform go)
	{
		if (go == null) return;
		for (int i = go.childCount - 1; i >= 0; i--)
		{
			GameObject.Destroy(go.GetChild(i).gameObject);
		}
	}
}
