using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine machine;

    public void SetTransition(StateID stateID) { machine.PerformTransition(stateID); }

    public void Start()
    {
        SetupStateMachine();
    }

    public void Update()
    {
        machine.CurrentState.Reason(gameObject);
        machine.CurrentState.Act(gameObject);
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
