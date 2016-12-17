using System;
using InControl;
using UnityEngine;

public class CrouchState : State, IStateVisitor {


    public CrouchState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.CrouchState) {}

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
        player.GetComponent<Player>().SetTransition(StateID.IdleState);
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Crouch State");
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Crouch State");
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Crouch State");
    }

	protected override void Conclude(GameObject player)
	{

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
