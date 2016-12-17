using System;
using UnityEngine;
using InControl;

public class AttackState : State, IStateVisitor
{
	private bool performed = true;

    public AttackState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.AttackState) { }

	public override void DoBeforeEntering() {
		base.DoBeforeEntering();
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
				otherPlayer.GetComponent<Player> ().machine.CurrentState.Accept(this);
			}

			performed = true;
		}
    }

	protected override void Conclude(GameObject player)
	{
		Debug.Log("Attack concluded");
	}

	protected override void Prepare(GameObject player, InputDevice inputDevice)
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
