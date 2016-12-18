using System;
using UnityEngine;
using InControl;

public class IdleState : State, IStateVisitor {

    public IdleState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.IdleState) { }

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
        var range = player.GetComponent<Range>();
        var rangePhone = range.phoneInRange;

        if (rangePhone != null)
        {
            foreach (var aPlayer in GameObject.FindGameObjectsWithTag("Player"))
            {
                aPlayer.GetComponent<PhoneHand>().hasPhone = false;
                aPlayer.GetComponent<Range>().phoneInRange = null;
            }

            rangePhone.gameObject.SetActive(false);
            player.GetComponent<PhoneHand>().hasPhone = true;
        }
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        
    }

	protected override void Conclude(GameObject player)
	{

	}

	public override void Accept(IStateVisitor other)
	{
		other.Visit(this);
	}
}
