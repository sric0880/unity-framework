using UnityEngine;
using System.Collections;
using NodeEditorFramework;

namespace UIFramework.Editor
{
	[Node(false, "Exclusive Node")]
	public class UIExclusiveNode : Node
	{
		public string nodeName;

		public override string GetID { get { return "UIExclusiveNode"; } }

		public override Node Create(Vector2 pos)
		{
			UIExclusiveNode node = CreateInstance<UIExclusiveNode>();

			node.rect = new Rect(pos.x, pos.y, 200, 80);
			node.name = "Exclusive Node";

			// Some Connections
			node.CreateInput("parent", "NormalChildren", NodeSide.Left, 20);
			node.CreateOutput("children", "NormalChildren", NodeSide.Right, 20);

			return node;
		}

		protected internal override void DrawNode()
		{
			Rect nodeRect = rect;
			nodeRect.position += NodeEditor.curEditorState.zoomPanAdjust + NodeEditor.curEditorState.panOffset;
			contentOffset = new Vector2(0, 40);

			GUI.BeginGroup(nodeRect, GUI.skin.box);

			Rect headerRect = new Rect(0, 0, nodeRect.width, contentOffset.y);
			GUILayout.BeginArea(headerRect, NodeEditorGUI.nodeBoxHeader2);
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
		}

		public override bool Calculate()
		{
			return true;
		}
	}
}