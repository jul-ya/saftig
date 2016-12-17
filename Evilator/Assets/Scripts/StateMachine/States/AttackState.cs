using System;
using UnityEngine;
using InControl;

public class AttackState : State, IStateVisitor
{

    public AttackState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.AttackState) { }

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
        if(currentTime> prepareTime + performTime + cooldownTime)
        {
            currentTime = 0.0f;
            player.GetComponent<Player>().SetTransition(StateID.IdleState);
        }
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
		var otherPlayers = player.GetComponent<Range> ().playersInRange;

		foreach(var otherPlayer in otherPlayers) {

		}
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        
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
