using UnityEngine;
using InControl;

public class Player : MonoBehaviour
{
    private StateMachine machine;
    private InputDevice inputDevice;


    public InputDevice InputDevice
    {
        get { return inputDevice; }
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

	public void Attack()
	{
		if(machine.CurrentStateID == StateID.IdleState) {
			machine.PerformTransition(StateID.AttackState);
		}
	}

	public void Block()
	{
		if(machine.CurrentStateID == StateID.IdleState) {
			machine.PerformTransition(StateID.BlockState);
		}
	}

    private void SetupStateMachine()
    {
        IdleState idle = new IdleState(0.0f, 3.0f, 0.0f);
        AttackState attack = new AttackState(2.0f, 5.0f, 4.2f);

        machine = new StateMachine();
        machine.AddState(idle);
        machine.AddState(attack);
		machine.PerformTransition(StateID.IdleState);
    }
}
