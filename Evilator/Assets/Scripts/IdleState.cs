using System;
using UnityEngine;

public class IdleState : State {

    public IdleState()
    {
        stateID = StateID.IdleState;
    }

    public override void Act(GameObject player)
    {
        Debug.Log("idle state");
    }

    public override void Reason(GameObject player)
    {
        
    }
}
