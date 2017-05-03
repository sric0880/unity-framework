using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBootState : State
{
	public override void EnterState(FSM fsm, State fromState)
	{
		ConfigReader.ReadConfigAsBin(typeof(LaunchConfig), "launch_conf");
		//TODO: from launch config
		Log.Init(Log.Tag.Debug, true, true);
	}

	public override void LeaveState(FSM fsm, State toState)
	{
		
	}

	public override void Update(FSM fsm)
	{
		
	}
}
