using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class TypingState : State, IStateVisitor
{
    public TypingState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.TypingState) {}

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
        player.GetComponent<Player>().SetTransition(StateID.IdleState);
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Typing State");
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Typing State");
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Typing State");
    }

	public void Visit(AttackState attack)
	{
	}

	public void Visit(BlockState block)
	{
	}

	public void Visit(CrouchState crouch)
	{
	}

	public void Visit(IdleState idle)
	{
	}

	public void Visit(TypingState typing)
	{
	}

	public void Accept(IStateVisitor other)
	{
		other.Visit(this);
	}
}