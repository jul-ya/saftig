using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class StunState : State {
    public StunState(float prepareTime, float performTime, float cooldownTime) : base(prepareTime, performTime, cooldownTime, StateID.StunState){}

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
        player.GetComponent<Player>().SetTransition(StateID.IdleState);
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("stun cooldown State");
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Crouch State");
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("stun prepare State");
    }

	protected override void Conclude(GameObject player)
	{

	}
}
