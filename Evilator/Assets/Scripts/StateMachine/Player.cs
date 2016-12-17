using UnityEngine;
using InControl;

public class Player : MonoBehaviour
{
    private StateMachine machine;
    private InputDevice inputDevice;


    public InputDevice InputDevice
    {
        set { inputDevice = value; }
    }

    public void SetTransition(StateID stateID) { machine.PerformTransition(stateID); }

    public void Start()
    {
        
        SetupStateMachine();
    }

    public void Update()
    {
        machine.CurrentState.Reason(gameObject, inputDevice);
        machine.CurrentState.Act(gameObject, inputDevice);
    }

    private void SetupStateMachine()
    {
        IdleState idle = new IdleState();

        AttackState attack = new AttackState();

        machine = new StateMachine();
        machine.AddState(idle);
        machine.AddState(attack);
    }
}
