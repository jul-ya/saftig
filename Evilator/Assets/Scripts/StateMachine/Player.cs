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
        inputDevice = GetComponent<Controls>().dev;
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
		IdleState idle = new IdleState(0.0f, float.MaxValue, 0.0f);
        AttackState attack = new AttackState(0.15f, 0.5f, 0.2f);
		CrouchState crouch = new CrouchState(0.0f, float.MaxValue, 0.0f);
		BlockState block = new BlockState(0.0f, float.MaxValue, 0.0f);
		TypingState typing = new TypingState(0.0f, 0.4f, 0.2f);
		StunState stun = new StunState(0.0f, 5.0f, 0.0f);

        machine = new StateMachine();
        machine.AddState(idle);
        machine.AddState(attack);
		machine.AddState(crouch);
		machine.AddState(block);
		machine.AddState(typing);
		machine.AddState(stun);

		machine.PerformTransition(StateID.IdleState);
    }
}
