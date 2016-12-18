using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class BlockState : State, IStateVisitor {

    public BlockState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.BlockState) {}

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
		if(phase == Phase.Concluded)
		{
			machine.PerformTransition(StateID.IdleState);
		}
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("block state");
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("block state");
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("block state");
    }

	protected override void Conclude(GameObject player)
	{

	}

	public override void Accept(IStateVisitor other)
	{
		other.Visit(this);
	}
}
