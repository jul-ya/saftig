using InControl;
using UnityEngine;

public class CrouchState : State {


    public CrouchState()
    {
        stateID = StateID.CrouchState;
    }

    public override void Act(GameObject player, InputDevice inputDevice)
    {
        Debug.Log("Crouch State");
    }

    public override void Reason(GameObject player, InputDevice inputDevice)
    {
        player.GetComponent<Player>().SetTransition(StateID.IdleState);
    }

}
