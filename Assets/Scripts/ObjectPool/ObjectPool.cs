using System.Collections.Generic;

public class ObjectPool<T> where T : class, new()
{
	public delegate T CreateFunc();
	public delegate void ResetFunc(T obj);

	public ObjectPool(int capacity, CreateFunc createFunc = null, ResetFunc resetFunc = null)
	{
		pool = new Stack<T>(capacity);
		this.createFunc = createFunc;
		this.resetFunc = resetFunc;
	}

	public T Get()
	{
		if (pool.Count > 0)
		{
			return pool.Pop();
		}
		return (createFunc != null) ? createFunc() : new T();
	}

	public void Store(T obj)
	{
		if (obj == null)
		{
			return;
		}
		if (resetFunc != null)
		{
			resetFunc(obj);
		}
		pool.Push(obj);
	}

	public int Count
	{
		get
		{
			return pool.Count;
		}
	}

	private Stack<T> pool = null;
	private ResetFunc resetFunc = null;
	private CreateFunc createFunc = null;
}