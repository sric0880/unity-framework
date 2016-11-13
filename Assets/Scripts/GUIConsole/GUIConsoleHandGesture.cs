using UnityEngine;

[RequireComponent(typeof(GUIConsole))]
public class GUIConsoleHandGesture : MonoBehaviour
{
	/// 划屏密码:屏幕[0,1]空间分为9个区域。每个区域是一个直径0.3的圆。
	/// HitArea定义，1~9分别是9个圆HitArea，0表示没有碰到任何圆
	/// | 1 2 3 |
	/// | 4 5 6 |
	/// | 7 8 9 | 
	/// 
	// 密码： 上上下下左右左右BA（BA都是右下），在BA的位置松手
	private static readonly int[] Code = { 5, 2, 5, 2, 5, 8, 5, 8, 5, 4, 5, 6, 5, 4, 5, 6, 5, 9, 5, 9 };

	private static readonly Vector2[] centers =
		{
			new Vector2(0.15f, 0.85f),
			new Vector2(0.5f, 0.85f),
			new Vector2(0.85f, 0.85f),
			new Vector2(0.15f, 0.5f),
			new Vector2(0.5f, 0.5f),
			new Vector2(0.85f, 0.5f),
			new Vector2(0.15f, 0.15f),
			new Vector2(0.5f, 0.15f),
			new Vector2(0.85f, 0.15f)
		};
	private const float radios = 0.15f;

	private bool inputIsCorrect;
	private int currentInputIndex;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			StartInput();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			EndInput();
		}
		else if (Input.GetMouseButton(0) && inputIsCorrect)
		{
			CheckInput();
		}
	}

	private void StartInput()
	{
		if (GetHitArea() == Code[0])
		{
			inputIsCorrect = true;
			currentInputIndex = 0;
		}
		else
		{
			inputIsCorrect = false;
			currentInputIndex = 0;
		}
	}

	private void CheckInput()
	{
		if (!inputIsCorrect)
			return;

		var hitArea = GetHitArea();
		if (hitArea == 0 || hitArea == Code[currentInputIndex])
		{
			return;
		}
		else if (currentInputIndex + 1 < Code.Length && hitArea == Code[currentInputIndex + 1])
		{
			++currentInputIndex;
		}
		else
		{
			inputIsCorrect = false;
			currentInputIndex = 0;
		}
	}

	private void EndInput()
	{
		if (inputIsCorrect && currentInputIndex == Code.Length - 1)
		{
			GetComponent<GUIConsole>().enabled = true;
		}

		inputIsCorrect = false;
		currentInputIndex = 0;
	}

	private int GetHitArea()
	{
		Vector2 pos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
		for (int i = 0; i < centers.Length; ++i)
		{
			if ((pos - centers[i]).magnitude < radios)
				return i + 1;
		}
		return 0;
	}
}