using System;
using UnityEngine;
using InControl;

public class IdleState : State, IStateVisitor {

    public IdleState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.IdleState) { }

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
        if (currentTime > prepareTime + performTime + cooldownTime)
        {
            currentTime = 0.0f;
        }
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        
       
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
        
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
