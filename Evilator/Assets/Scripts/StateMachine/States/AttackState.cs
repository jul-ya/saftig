using System;
using UnityEngine;
using InControl;

public class AttackState : State, IStateVisitor
{
	private bool performed = true;

    public AttackState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.AttackState) { }

	private GameObject thisPlayer;
	private GameObject otherPlayer;

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
			thisPlayer = player;

			foreach(var otherPlayer in otherPlayers) {
				// TODO if has phone, lose it
				this.otherPlayer = otherPlayer.gameObject;
				otherPlayer.GetComponent<Player> ().Accept(this);
			}

			thisPlayer = null;
			performed = true;
		}
    }

	protected override void Conclude(GameObject player)
	{
		
	}

	protected override void Prepare(GameObject player, InputDevice inputDevice)
	{
		
	}

	private void Stun(GameObject player) {
		player.GetComponent<Player> ().machine.PerformTransition(StateID.StunState);
	}

	public override void Visit(IdleState idle)
	{
		// Only opponent is stunned
		Stun(otherPlayer);
	}

	public override void Visit(TypingState typing)
	{
		Stun(otherPlayer);
	}
		
	public override void Visit(AttackState attack)
	{
		// Both are stunned
		Stun(thisPlayer);
		Stun(otherPlayer);
	}

	public override void Visit(BlockState block)
	{
		// When attacking a blocking player, nothing happens
	}

	public override void Visit(StunState stun)
	{
		//  When attacking a stunned player, nothing happens
	}

	public override void Visit(CrouchState crouch)
	{
		// When attacking a crouching player, nothing happens
	}

	public override void Accept(IStateVisitor other)
	{
		other.Visit(this);
	}
}
