using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class BlockState : State {

    public BlockState()
    {
        stateID = StateID.BlockState;
    }

    public override void Act(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("block state");
    }

    public override void Reason(GameObject player, InputDevice inputDevice)
    {
        player.GetComponent<Player>().SetTransition(StateID.IdleState);
    }

   
}
