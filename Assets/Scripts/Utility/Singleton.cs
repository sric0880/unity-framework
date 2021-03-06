public class Singleton<T> where T : Singleton<T>, new()
{
	private static T instance;

    public static T Instance
    {
        get
        {
			return instance ?? (instance = new T());
        }
    }
}

public abstract class MgrSingleton<T> : Singleton<T> where T : MgrSingleton<T>, new()
{
	public abstract void OnInit();
}