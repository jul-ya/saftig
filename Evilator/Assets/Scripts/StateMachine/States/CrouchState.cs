using System;
using InControl;
using UnityEngine;

public class CrouchState : State, IStateVisitor {


    public CrouchState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.CrouchState) {}

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
		if(phase == Phase.Concluded)
		{
			machine.PerformTransition(StateID.IdleState);
		}
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Crouch State");
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
		var range = player.GetComponent<Range> ();

		if(range.phoneInRange != null) {
			range.phoneInRange.gameObject.SetActive(false);
			player.GetComponent<PhoneHand> ().hasPhone = true;
		}
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Crouch State");
    }

	protected override void Conclude(GameObject player)
	{
		
	}

	public override void Accept(IStateVisitor other)
	{
		other.Visit(this);
	}
}
