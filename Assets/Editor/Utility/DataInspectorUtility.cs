using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

public class DataInspectorUtility{

	private static Dictionary<string, bool> foldoutPath = new Dictionary<string, bool>();
	private static HashSet<string> changedPath = new HashSet<string>();

	private readonly static EnumInspector enumInspector = new EnumInspector();
	private readonly static PrimitiveInspector primitiveInspector = new PrimitiveInspector();
	private readonly static ClassInspector classInspector = new ClassInspector();
	private readonly static ArrayInspector arrayInspector = new ArrayInspector();
	private readonly static ListInspector listInspector = new ListInspector();
	private readonly static DictionaryInspector dictInspector = new DictionaryInspector();
	private readonly static DateTimeInspector dateTimeInspector = new DateTimeInspector();
	private readonly static DecimalInspector decimalInspector = new DecimalInspector();

	public static bool inspect(ref object data, Type type, string name, string path = "")
	{
		if (type == null)
			type = data!=null? data.GetType() : null;
		DataInspector inspector = getInspector(type);
		bool changed = false;
		if (inspector != null)
		{
			path = path + "." + name;
			if (inspector.canFoldout())
			{
				EditorGUILayout.BeginHorizontal();
				Color color;
				bool changedColor = beginColor(path, out color);
				foldoutPath[path] = EditorGUILayout.Foldout(foldoutPath.ContainsKey(path) &&
				                                            foldoutPath[path], name);
				changed |= inspector.printHead(ref data, type);
				endColor(changedColor, color);
				EditorGUILayout.EndHorizontal();

				if (foldoutPath[path])
				{
					EditorGUI.indentLevel++;
					changed |= inspector.inspect(ref data, type, name, path);
					EditorGUI.indentLevel--;
				}
			}
			else 
			{
				Color color;
				bool changedColor = beginColor(path, out color);
				changed |= inspector.inspect(ref data, type, name, path);
				endColor(changedColor, color);
			}
			if (changed)
			{
				changedPath.Add(path);
			}
		}
		return changed;
	}

	private static bool beginColor(string path, out Color color)
	{
		bool containsChanged = false;
		color = GUI.backgroundColor;
		if (changedPath.Contains(path))
		{
			GUI.backgroundColor = Color.green;
			containsChanged = true;
		}
		return containsChanged;
	}
	private static void endColor(bool beginColor, Color color)
	{
		if (beginColor)
		{
			GUI.backgroundColor = color;
		}
	}

	public static DataInspector getInspector(Type type)
	{
		if (type.IsEnum)
			return enumInspector;
		if (type.IsPrimitive || type == typeof(string))
			return primitiveInspector;
		if (type.IsArray)
			return arrayInspector;
		if (type == typeof(DateTime))
			return dateTimeInspector;
		if (type == typeof(decimal))
			return decimalInspector;
		if (type.IsGenericType)
		{
			var typeDef = type.GetGenericTypeDefinition();
			if (typeDef == typeof(List<>))
				return listInspector;
			if (typeDef == typeof(Dictionary<,>))
				return dictInspector;

		}

		return classInspector;

	}

	public static void clearChangedPath()
	{
		changedPath.Clear();
	}

	public static void removeChangedPath(string oldPath)
	{
		if (changedPath.Contains(oldPath))
		{
			changedPath.Remove(oldPath);
		}
	}

	public static void addChangedPath(string newPath)
	{
		changedPath.Add(newPath);
	}

	public static void addChangedPath(string oldPath, string newPath)
	{
		if (changedPath.Contains(oldPath))
		{
			changedPath.Remove(oldPath);
			changedPath.Add(newPath);
		}
	}
}
