using UnityEngine;

public class GUIConsoleHeaderFps : GUIConsoleHeader
{
	private float m_LastUpdateShowTime = 0f;
	private float m_UpdateShowDeltaTime = 0.5f;
	private int m_FrameUpdate = 0;
	private float m_FPS = 0;

	public override void OnUpdate()
	{
		m_FrameUpdate++;
		if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
		{
			m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
			m_FrameUpdate = 0;
			m_LastUpdateShowTime = Time.realtimeSinceStartup;
		}
	}

	public override void OnGUI()
	{
		GUILayout.Label(string.Format("fps:{0:F1}", m_FPS));
	}
}