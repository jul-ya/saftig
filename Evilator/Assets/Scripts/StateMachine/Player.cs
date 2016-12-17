using UnityEngine;
using InControl;

public class Player : MonoBehaviour
{
    private StateMachine machine;
    private InputDevice inputDevice;
    public int digitsTyped;
    public string mumsPhoneNumber = "+436994242";

    public InputDevice InputDevice
    {
        get { return inputDevice; }
        set { inputDevice = value; }
    }

    public void SetTransition(StateID stateID) { machine.PerformTransition(stateID); }

    public void Start()
    {
        SetupStateMachine();
        digitsTyped = 0;
    }

    public void Update()
    {
        machine.CurrentState.Reason(gameObject, inputDevice);
        machine.CurrentState.Act(gameObject, inputDevice);
    }

    private void SetupStateMachine()
    {
        IdleState idle = new IdleState(0.0f, 3.0f, 0.0f);

        AttackState attack = new AttackState(2.0f, 5.0f, 4.2f);

        machine = new StateMachine();
        machine.AddState(idle);
        machine.AddState(attack);
    }
}
