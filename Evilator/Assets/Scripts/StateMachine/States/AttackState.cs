using System;
using UnityEngine;
using InControl;

public class AttackState : State
{

    public AttackState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.AttackState) { }

    public override void Reason(GameObject player, InputDevice inputDevice)
    {
        if(currentTime> prepareTime + performTime + cooldownTime)
        {
            player.GetComponent<Player>().SetTransition(StateID.IdleState);
        }
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("cooldown attack state");
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("attack state");
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("prepare attack state");
    }
}
