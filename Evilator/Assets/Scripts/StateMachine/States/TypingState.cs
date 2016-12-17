using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class TypingState : State
{

    public TypingState()
    {
        stateID = StateID.TypingState;
    }

    public override void Act(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Typing State");
    }

    public override void Reason(GameObject player, InputDevice inputDevice)
    {
        player.GetComponent<Player>().SetTransition(StateID.IdleState);
    }
}