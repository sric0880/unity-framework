using UniRx;

public class Transition
{
	private FSM fsm;
	private State targetState;
	private State sourceState;
	private ReactiveProperty<bool> condition;
	private IObservable<bool> asyncOpt; //状态切换执行的异步代码
	public State TargetState { get { return targetState; } }
	public State SourceState { get { return sourceState; } }

	public Transition(FSM fsm, State sourceState, State targetState, ReactiveProperty<bool> condition, IObservable<bool> asyncOpt)
	{
		this.fsm = fsm;
		this.sourceState = sourceState;
		this.targetState = targetState;
		this.condition = condition;
		this.asyncOpt = asyncOpt;
		this.condition.Subscribe(doTransition);
	}

	private void doTransition(bool yes)
	{
		if (yes && (sourceState == this.fsm.CurState || sourceState == this.fsm.AnyState))
		{
			if (this.asyncOpt == null)
			{
				this.fsm.CurState = targetState;
			}
			else {
				asyncOpt.Subscribe(ok =>
				{
					if (ok)
					{
						fsm.CurState = targetState;
					}
					else
					{
						Log.Error("状态切换过程中发生了意外");
					}
				});
			}
		}
	}

}