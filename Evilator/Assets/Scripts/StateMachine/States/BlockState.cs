using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class BlockState : State {

    public BlockState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.BlockState) {}

    protected override void DoReason(GameObject player, InputDevice inputDevice)
    {
        player.GetComponent<Player>().SetTransition(StateID.IdleState);
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("block state");
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("block state");
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("block state");
    }
}
