public abstract class GUIConsolePage
{
    public string PageName;

    public abstract void Start();
    public abstract void OnGUI();
	public abstract void Exit();
}