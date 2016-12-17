using System;
using UnityEngine;
using InControl;

public class AttackState : State, IStateVisitor
{
	private bool performed = true;

    public AttackState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.AttackState) { }

	public override void DoBeforeEntering() {
		performed = false;
	}

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
		if(phase == Phase.Concluded)
		{
			currentTime = 0.0f;
			machine.PerformTransition(StateID.IdleState);
		}
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
		
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
		if(!performed) {
			var otherPlayers = player.GetComponent<Range> ().playersInRange;

			foreach(var otherPlayer in otherPlayers) {
				// TODO if has phone, lose it

				otherPlayer.GetComponent<Player> ().SetTransition(StateID.StunState);
			}

			performed = true;
		}
    }

	protected override void Conclude(GameObject player)
	{
		
	}

	protected override void Prepare(GameObject player, InputDevice inputDevice)
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
