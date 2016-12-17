using System;
using InControl;
using UnityEngine;

public class CrouchState : State, IStateVisitor {


    public CrouchState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.CrouchState) {}

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
		
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

	public override void Accept(IStateVisitor other)
	{
		other.Visit(this);
	}
}
