using System;
using UnityEngine;
using InControl;

public class IdleState : State, IStateVisitor {

    public IdleState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.IdleState) { }

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
        if (currentTime > prepareTime + performTime + cooldownTime)
        {
            currentTime = 0.0f;
            player.GetComponent<Player>().SetTransition(StateID.AttackState);
        }
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("idle cooldown state");
       
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("idle state");
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("idle prepare state");
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
