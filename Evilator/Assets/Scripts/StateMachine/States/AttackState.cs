using System;
using UnityEngine;
using InControl;

public class AttackState : State, IStateVisitor
{
	private bool performed = true;

    public AttackState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.AttackState) { }

	private GameObject thisPlayer;
	private GameObject otherPlayer;
	private GameObject phone;

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
		AddPhoneImpulse();
	}

	public override void Visit(TypingState typing)
	{
		Stun(otherPlayer);
		AddPhoneImpulse();
	}
		
	public override void Visit(AttackState attack)
	{
		// Both are stunned
		Stun(thisPlayer);
		Stun(otherPlayer);
		AddPhoneImpulse();
	}

	public override void WasAddedTo(StateMachine machine) {
		base.WasAddedTo(machine);
	}

	public void obtainPhone() {
		phone = GameObject.FindGameObjectWithTag("Phone");
		Debug.Log("Phone: " + phone);
	}

	/**
	 * Throws phone out of hand
	 */
	private void AddPhoneImpulse() {
		var hand = otherPlayer.GetComponent<PhoneHand> ();
		if(hand.hasPhone) {
			hand.hasPhone = false;

			phone.transform.position = hand.phonePosition;
			phone.SetActive(true);

			phone.GetComponent<PhoneFlight> ().StartFlight(phone);

		}
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
