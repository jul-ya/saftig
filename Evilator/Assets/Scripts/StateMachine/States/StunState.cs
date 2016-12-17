using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class StunState : State {
    public StunState(float prepareTime, float performTime, float cooldownTime) : base(prepareTime, performTime, cooldownTime, StateID.StunState){}

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
        
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {

    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        
    }

	protected override void Conclude(GameObject player)
	{
        machine.PerformTransition(StateID.IdleState);
	}
}
