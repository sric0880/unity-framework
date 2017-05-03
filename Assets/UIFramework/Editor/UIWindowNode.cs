using UnityEngine;
using System.Collections;
using NodeEditorFramework;

namespace UIFramework.Editor
{
	[Node(false, "Window Node")]
	public class UIWindowNode : Node
	{
		public string nodeName;
		public string prefabName;
		public bool lockBg;
		public bool darkenBg;
		public bool blurBg;
		public bool clickBgToClose;

		public override string GetID { get { return "UIWindowNode"; } }

		public override Node Create(Vector2 pos)
		{
			UIWindowNode node = CreateInstance<UIWindowNode>();

			node.rect = new Rect(pos.x, pos.y, 200, 170);
			node.name = "Window Node";

			// Some Connections
			node.CreateInput("parent", "NormalChildren", NodeSide.Left, 20);

			return node;
		}
		protected internal override void DrawNode()
		{
			Rect nodeRect = rect;
			nodeRect.position += NodeEditor.curEditorState.zoomPanAdjust + NodeEditor.curEditorState.panOffset;
			contentOffset = new Vector2(0, 40);

			GUI.BeginGroup(nodeRect, GUI.skin.box);

			Rect headerRect = new Rect(0, 0, nodeRect.width, contentOffset.y);
			GUILayout.BeginArea(headerRect, NodeEditorGUI.nodeBoxHeader3);
			GUI.Label(headerRect, name, NodeEditor.curEditorState.selectedNode == this ? NodeEditorGUI.nodeLabelBold : NodeEditorGUI.nodeLabel);
			GUILayout.EndArea();

			GUI.changed = false;
			Rect bodyRect = new Rect(0, contentOffset.y, nodeRect.width, nodeRect.height - contentOffset.y);
			GUILayout.BeginArea(bodyRect, NodeEditorGUI.nodeBoxBody);
			NodeGUI();
			GUILayout.EndArea();
			GUI.EndGroup();
		}

		protected internal override void NodeGUI()
		{
			nodeName = UILayoutHelper.TextFiled("Name:", nodeName, this);
			prefabName = UILayoutHelper.TextFiled("PrefabName:", prefabName, this);
			lockBg = GUILayout.Toggle(lockBg, "Lock Bg");
			darkenBg = GUILayout.Toggle(darkenBg, "Darken Bg");
			blurBg = GUILayout.Toggle(blurBg, "blur Bg");
			clickBgToClose = GUILayout.Toggle(clickBgToClose, "Click to close bg");
		}

		public override bool Calculate()
		{
			return true;
		}
	}
}