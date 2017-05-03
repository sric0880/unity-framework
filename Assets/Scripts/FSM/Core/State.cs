using System.Collections;
using System.Collections.Generic;

public abstract class State {
	
	public abstract void EnterState(FSM fsm, State fromState);

	public abstract void LeaveState(FSM fsm, State toState);

	public abstract void Update(FSM fsm);
}
