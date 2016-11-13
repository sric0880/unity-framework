using System;

public abstract class DataInspector {

	public abstract bool canFoldout();

	public virtual bool printHead(ref object data, Type type) { return false; }

	public abstract bool inspect(ref object data, Type type, string name, string path);

	protected bool applyData(ref object data, object newData)
	{
		if (data == null)
		{
			if (newData == null) return false;
			else {
				data = newData;
				return true;
			}
		}
		else 
		{
			bool changed = !data.Equals(newData);
			if (changed) data = newData;
			return changed;
		}

	}
}
