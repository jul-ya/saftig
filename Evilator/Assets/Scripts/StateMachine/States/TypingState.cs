﻿using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class TypingState : State
{
    public TypingState(float prepareTime, float performTime, float cooldownTime):base(prepareTime, performTime, cooldownTime, StateID.TypingState) {}

    public override void Reason(GameObject player, InputDevice inputDevice)
    {
        player.GetComponent<Player>().SetTransition(StateID.IdleState);
    }

    protected override void Cooldown(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Typing State");
    }

    protected override void PerformAction(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Typing State");
    }

    protected override void Prepare(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Typing State");
    }
}