﻿using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class TypingState : State, IStateVisitor
{
    public TypingState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.TypingState) {}

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
		if(phase == Phase.Concluded)
		{
			machine.PerformTransition(StateID.IdleState);
		}
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        //Debug.Log("Typing State");
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
        //Debug.Log("Typing State");
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        //Debug.Log("Typing State");
    }

	protected override void Conclude(GameObject player)
	{

	}

	public override void Visit(AttackState attack)
	{
	}

	public override void Visit(BlockState block)
	{
	}

	public override void Visit(CrouchState crouch)
	{
	}

	public override void Visit(IdleState idle)
	{
	}

	public override void Visit(TypingState typing)
	{
	}

	public override void Accept(IStateVisitor other)
	{
		other.Visit(this);
	}
}