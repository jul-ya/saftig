using System;
using UnityEngine;
using InControl;

public class IdleState : State {

    public IdleState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.IdleState) { }

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
        if (currentTime > prepareTime + performTime + cooldownTime)
        {
            currentTime = 0.0f;
            player.GetComponent<Player>().SetTransition(StateID.AttackState);
        }
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("idle cooldown state");
       
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("idle state");
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("idle prepare state");
    }
}
