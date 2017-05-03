using UnityEngine;
using UnityEditor;
using System;
using UniRx;
using System.Collections.Generic;
using System.Reflection;

public class FSMInspector : EditorWindow{

	[MenuItem("Window/Inspector/FSM")]
	private static void OpenConfigEditor()
	{
		FSMInspector window = (FSMInspector)EditorWindow.GetWindow(typeof(FSMInspector));
		window.Show();
	}

	private Vector2 scrollPos;
	private Type[] FSMType = new Type[1]{ typeof(GameFSM), };
	private bool[] foldout = new bool[1];
	private class FSMTriggerData {
		public List<string> triggerNames = new List<string>();
		public List<MethodInfo> triggerFuncs = new List<MethodInfo>();
		public List<object> triggers = new List<object>();
		public int selectedTrigger = -1;
		public int lastSelecteTrigger = -1;

		public void Clear()
		{
			triggerNames.Clear();
			triggerFuncs.Clear();
			triggers.Clear();
		}

		public void Trigger()
		{
			if (lastSelecteTrigger != selectedTrigger)
			{
				triggerFuncs[selectedTrigger].Invoke(triggers[selectedTrigger], null);
				lastSelecteTrigger = selectedTrigger;
			}
		}

		public void OnGUI()
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(30);
			selectedTrigger = GUILayout.SelectionGrid(selectedTrigger, triggerNames.ToArray(), 1, EditorStyles.radioButton);
			EditorGUILayout.EndHorizontal();
		}
	}
	private FSMTriggerData[] data = new FSMTriggerData[1] { new FSMTriggerData() };

	private void OnGUI()
	{
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		for (int i = 0; i < FSMType.Length; ++i)
		{
			Type type = FSMType[i];
			foldout[i] = EditorGUILayout.Foldout(foldout[i], type.Name);
			if (foldout[i])
			{
				var obj = type.GetProperty("Instance").GetValue(null, null);
				if (obj == null) continue;

				EditorGUILayout.LabelField("    All Triggers: ", GUILayout.Width(100));

				var fields = type.GetFields();

				data[i].Clear();
				foreach (var field in fields)
				{
					if (field.FieldType == typeof(BoolTrigger))
					{
						data[i].triggerNames.Add(field.Name);
						var trigger = field.GetValue(obj);
						var func = typeof(BoolTrigger).GetMethod("Trigger");
						data[i].triggers.Add(trigger);
						data[i].triggerFuncs.Add(func);
					}
				}
				data[i].OnGUI();
				data[i].Trigger();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("    Current State: ", GUILayout.Width(100));
				var curstate = type.GetProperty("CurState").GetValue(obj, null);
				if (curstate != null)
				{
					Color oldColor = GUI.color;
					GUI.color = Color.green;
					EditorGUILayout.LabelField(curstate.GetType().Name);
					GUI.color = oldColor;
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("    Last State: ", GUILayout.Width(100));
				var laststate = type.GetProperty("LastState").GetValue(obj, null);
				if (laststate != null)
				{
					Color oldColor = GUI.color;
					GUI.color = Color.green;
					EditorGUILayout.LabelField(laststate.GetType().Name);
					GUI.color = oldColor;
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.LabelField("    All States: ", GUILayout.Width(100));

				foreach (var field in fields)
				{
					if (field.FieldType.IsSubclassOf(typeof(State)))
					{
						EditorGUILayout.BeginHorizontal();
						GUILayout.Space(30);
						EditorGUILayout.LabelField(field.FieldType.Name);
						EditorGUILayout.EndHorizontal();
					}
				}
			}
		}


		EditorGUILayout.EndScrollView();
	}
}
