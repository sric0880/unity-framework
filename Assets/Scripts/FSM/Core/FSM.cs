using UniRx;
using UnityEngine;
using System.Collections.Generic;

public class FSM {
	private List<Transition> transitions = new List<Transition>();
	public List<Transition> Transitions
	{
		get { return transitions; }
	}

	private State lastState;
	public State LastState
	{
		get { return lastState; }
	}

	private State curState;
	public State CurState
	{
		get { return curState; }
		set { curState = value; }
	}

	private State lastAnyState;
	public State LastAnyState
	{
		get { return lastAnyState; }
	}

	private State anyState;
	public State AnyState
	{
		get { return anyState; }
		set { anyState = value; }
	}

	public void Update()
	{
		if (anyState != lastAnyState)
		{
			if (lastAnyState != null)
			{
				lastAnyState.LeaveState(this, anyState);
			}
			lastAnyState = anyState;
			if (anyState != null)
			{
				anyState.EnterState(this, lastAnyState);
			}
		}
		if (anyState != null)
		{
			anyState.Update(this);
		}

		if (curState != lastState) // Switch state
		{
			if (lastState != null)
			{
				lastState.LeaveState(this, curState);
			}
			var templaststate = lastState;
			lastState = curState;
			if (curState != null)
			{
				curState.EnterState(this, templaststate);
				if (curState != lastState) {
					Log.Error("Cannot change state in method of EnterState");
					curState = lastState;
				}
			}
		}
		if (curState != null && curState != anyState)
		{
			curState.Update(this);
		}
	}

	public void AddTransition(State sourceState, State targetState, ReactiveProperty<bool> condition, IObservable<bool> asyncOpt)
	{
		var trans = GetTransition(sourceState, targetState);
		if (trans == null)
		{
			trans = new Transition(this, sourceState, targetState, condition, asyncOpt);
		}
		if (transitions.Contains(trans))
			return;
		transitions.Add(trans);
	}

	public void RemoveTransition(State sourceState, State targetState)
	{
		var trans = GetTransition(sourceState, targetState);
		if (trans == null || !transitions.Contains(trans))
			return;
		transitions.Remove(trans);
	}

	public Transition GetTransition(State sourceState, State targetState)
	{
		for (int i = 0; i < transitions.Count; i++)
		{
			var t = transitions[i];
			if (t.TargetState == targetState && t.SourceState == sourceState)
				return t;
		}
		return null;
	}
}
