﻿using System;
using UnityEngine;

public class AttackState : State
{

    public AttackState()
    {
        stateID = StateID.AttackState;
    }

    public override void Act(GameObject player)
    {
        Debug.Log("attack state");
    }

    public override void Reason(GameObject player)
    {
        player.GetComponent<Player>().SetTransition(Transition.EndAttack);
    }
}