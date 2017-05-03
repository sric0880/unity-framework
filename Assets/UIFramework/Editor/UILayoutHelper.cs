using System;
using UnityEngine;
using NodeEditorFramework;

namespace UIFramework.Editor
{
	public static class UILayoutHelper
	{
		public static string TextFiled(string label, string text, Node node)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(label);
			var labelRect = GUI.skin.label.CalcSize(new GUIContent(label));
			if (text == null) text = string.Empty;
			text = GUILayout.TextField(text);
			var textRect = GUI.skin.label.CalcSize(new GUIContent(text));
			var newWidth = textRect.x + 40 + labelRect.x;
			if (newWidth > node.rect.width)
			{
				node.rect.width = newWidth;
			}
			GUILayout.EndHorizontal();
			return text;
		}
	}

	// Connection Type only for visual purposes
	public class RequiredChildType : IConnectionTypeDeclaration
	{
		public string Identifier { get { return "RequiredChildren"; } }
		public Type Type { get { return typeof(string); } }
		public Color Color { get { return Color.green; } }
		public string InKnobTex { get { return "Textures/In_Knob.png"; } }
		public string OutKnobTex { get { return "Textures/Out_Knob.png"; } }
	}

	public class NormalChildType : IConnectionTypeDeclaration
	{
		public string Identifier { get { return "NormalChildren"; } }
		public Type Type { get { return typeof(string); } }
		public Color Color { get { return Color.white; } }
		public string InKnobTex { get { return "Textures/In_Knob.png"; } }
		public string OutKnobTex { get { return "Textures/Out_Knob.png"; } }
	}
}
