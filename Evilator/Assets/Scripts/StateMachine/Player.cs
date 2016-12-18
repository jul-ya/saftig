using UnityEngine;
using InControl;

public class Player : MonoBehaviour
{
	public float attackPrepareTime = 0.15f;
	public float attackPerformTime = 0.5f;
	public float attackCooldownTime = 0.2f;

	public float typingPerformTime = 0.4f;
	public float typingCooldownTime = 0.2f;

	public float blockPrepareTime = 0.0f;
	public float blockPerformTime = 0.5f;
	public float blockCooldownTime = 0.0f;

	public float stunTime = 1.8f;

    public StateMachine machine;
    private InputDevice inputDevice;
    public int nrOfDigitsTyped;
    public string mumsPhoneNumber = "+436994242";

    public InputDevice InputDevice
    {
        get { return inputDevice; }
        set { inputDevice = value; }
    }

    public void SetTransition(StateID stateID) { machine.PerformTransition(stateID); }

	public void Accept(IStateVisitor visitor) {
		machine.CurrentState.Accept(visitor);
	}

    public void Start()
    {
        inputDevice = GetComponent<Controls>().dev;
        SetupStateMachine();
        nrOfDigitsTyped = 0;
    }

    public void Update()
    {
        machine.CurrentState.Reason(gameObject, inputDevice);
        machine.CurrentState.Act(gameObject, inputDevice);
    }

	public void Attack()
	{
		if(machine.CurrentStateID == StateID.IdleState && !GetComponent<PhoneHand> ().hasPhone) {
			machine.PerformTransition(StateID.AttackState);
		}
	}

	public void Block()
	{
		if(machine.CurrentStateID == StateID.IdleState) {
			machine.PerformTransition(StateID.BlockState);
		}
	}

	public void Uncrouch()
	{
		machine.PerformTransition(StateID.IdleState);
	}

    private void SetupStateMachine()
    {
		IdleState idle = new IdleState(0.0f, float.MaxValue, 0.0f);
		AttackState attack = new AttackState(attackPrepareTime, attackPerformTime, attackCooldownTime);
		attack.obtainPhone();
		BlockState block = new BlockState(blockPrepareTime, blockPerformTime, blockCooldownTime);
		TypingState typing = new TypingState(0.0f, typingPerformTime, typingCooldownTime);
		StunState stun = new StunState(0.0f, stunTime, 0.0f);

        machine = new StateMachine();
        machine.AddState(idle);
        machine.AddState(attack);
		machine.AddState(block);
		machine.AddState(typing);
		machine.AddState(stun);

		machine.PerformTransition(StateID.IdleState);
    }
}
