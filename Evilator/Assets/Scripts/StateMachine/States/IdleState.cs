using System;
using UnityEngine;
using InControl;

public class IdleState : State {

    public IdleState()
    {
        stateID = StateID.IdleState;
    }

    public override void Act(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("idle state");
    }

    public override void Reason(GameObject player, InputDevice inputDevice)
    {
        player.GetComponent<Player>().SetTransition(StateID.AttackState);
    }
}
