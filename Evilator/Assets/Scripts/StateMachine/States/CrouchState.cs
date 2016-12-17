using System;
using InControl;
using UnityEngine;

public class CrouchState : State {


    public CrouchState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.CrouchState) { }

    public override void Reason(GameObject player, InputDevice inputDevice)
    {
        player.GetComponent<Player>().SetTransition(StateID.IdleState);
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
}
