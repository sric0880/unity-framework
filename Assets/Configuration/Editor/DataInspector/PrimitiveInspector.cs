using UnityEditor;
using System;

public class PrimitiveInspector : DataInspector{

	public override bool canFoldout()
	{
		return false;
	}
	
	public override bool inspect(ref object data, Type type, string name, string path)
	{
		object newData = null;
		if (data is int) //Int32
		{
			newData = EditorGUILayout.IntField(name, (int)data);
		}
		else if (data is uint) //UInt32
		{
			newData = (uint)EditorGUILayout.IntField(name, (int)data);
		}
		else if (data is float) //Single
		{
			newData = EditorGUILayout.FloatField(name, (float)data);
		}
		else if (data is double) //Double
		{
			newData = EditorGUILayout.DoubleField(name, (double)data);
		}
		else if (data is bool) //Boolean
		{
			newData = EditorGUILayout.Toggle(name, (bool)data);
		}
		else if (data is char) //Char
		{
			char temp;
			var suc = char.TryParse(EditorGUILayout.TextField(name, data.ToString()), out temp);
			if(suc) newData = temp;
			else newData = data;
		}
		else if (data is short) //Int16
		{
			int temp = Convert.ToInt32(data);
			newData = (short)EditorGUILayout.IntField(name, temp);
		}
		else if (data is ushort) //UInt64
		{
			int temp = Convert.ToInt32(data);
			newData = (ushort)EditorGUILayout.IntField(name, temp);
		}
		else if (data is long) //Int64
		{
			long temp;
			var suc = long.TryParse(EditorGUILayout.TextField(name, data.ToString()), out temp);
			if (suc) newData = temp;
			else newData = data;
		}
		else if (data is ulong) //UInt64
		{
			ulong temp;
			var suc = ulong.TryParse(EditorGUILayout.TextField(name, data.ToString()), out temp);
			if (suc) newData = temp;
			else newData = data;
		}
		else if (data is byte) //Byte
		{
			byte temp;
			var suc = byte.TryParse(EditorGUILayout.TextField(name, data.ToString()), out temp);
			if (suc) newData = temp;
			else newData = data;
		}
		else if (data is string)
		{
			newData = EditorGUILayout.TextField(name, data.ToString());
		}
		else 
		{
			UnityEngine.Debug.LogError("Primitive type cannot be inspected");
			return false;
		}
		return applyData(ref data,newData);
	}
}
