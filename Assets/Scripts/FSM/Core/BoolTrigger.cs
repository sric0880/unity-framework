using UniRx;
public class BoolTrigger : BoolReactiveProperty
{
	public BoolTrigger(bool b) : base(b) {}
	public void Trigger()
	{
		Value = true;
		Value = false;
	}
}
