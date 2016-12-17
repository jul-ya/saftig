using System;
using UnityEngine;
using InControl;

public class AttackState : State
{

    public AttackState()
    {
        stateID = StateID.AttackState;
    }

    public override void Act(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("attack state");
    }

    public override void Reason(GameObject player, InputDevice inputDevice)
    {
        player.GetComponent<Player>().SetTransition(StateID.IdleState);
    }
}
