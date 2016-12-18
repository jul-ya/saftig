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

            if (otherPlayers.Count == 0)
                playRandom("fail");

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
        player.GetComponentInChildren<ParticleSystem>().Emit(20);
		player.GetComponent<Player> ().machine.PerformTransition(StateID.StunState);
	}

	public override void Visit(IdleState idle)
	{
        playRandom("succeed");
        // Only opponent is stunned
        Stun(otherPlayer);
		AddPhoneImpulse(otherPlayer);
	}

	public override void Visit(TypingState typing)
	{
        playRandom("succeed");
        Stun(otherPlayer);
		AddPhoneImpulse(otherPlayer);
	}
		
	public override void Visit(AttackState attack)
	{
        playRandom("succeed");
        // Both are stunned
        Stun(thisPlayer);
		Stun(otherPlayer);
		AddPhoneImpulse(otherPlayer.GetComponent<PhoneHand> ().hasPhone ? otherPlayer : thisPlayer);
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
	private void AddPhoneImpulse(GameObject phoneOwner) {
		var hand = phoneOwner.GetComponent<PhoneHand> ();
		if(hand.hasPhone) {
			hand.hasPhone = false;

			phone.transform.position = hand.phonePosition;
			phone.SetActive(true);

			phone.GetComponent<PhoneFlight> ().StartFlight(phone);

			phoneOwner.SendMessageUpwards("deactivatePhone");

		}

		thisPlayer.GetComponent<PhoneHand> ().hasPhone = false;
	}

	public override void Visit(BlockState block)
	{
        // When attacking a blocking player, nothing happens
        playRandom("succeed");
        Stun(thisPlayer);
	}

	public override void Visit(StunState stun)
	{
        //  When attacking a stunned player, nothing happens
        playRandom("succeed");
	}

	public override void Visit(CrouchState crouch)
	{
        // When attacking a crouching player, nothing happens
        playRandom("fail");
	}

	public override void Accept(IStateVisitor other)
	{
		other.Visit(this);
	}

    public void playRandom(String category)
    {
        MultipleAudioclips[] clips = thisPlayer.GetComponents<MultipleAudioclips>();
        if (clips[0].AudioCategory.Equals(category))
            clips[0].PlayRandomClip();
        else
            clips[1].PlayRandomClip();
    }
}
