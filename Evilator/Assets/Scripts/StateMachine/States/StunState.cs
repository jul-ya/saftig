using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class StunState : State {
    public StunState(float prepareTime, float performTime, float cooldownTime) : base(prepareTime, performTime, cooldownTime, StateID.StunState){}

	private PlayerPhysics disabledPhysics;

	public override void DoBeforeEntering() {
		
	}

	public override void DoBeforeLeaving() {
		
	}

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
        
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
		Debug.Log("DISABLED");

		disabledPhysics = player.GetComponent<PlayerPhysics> ();
		disabledPhysics.enabled = false;
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        
    }

	protected override void Conclude(GameObject player)
	{
		Debug.Log("ENABLED");

		disabledPhysics.enabled = true;
		disabledPhysics = null;

		machine.PerformTransition(StateID.IdleState);
	}
}
