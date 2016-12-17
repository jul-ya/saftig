using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public GameObject player;
    public Transform[] path;
    private StateMachine machine;

    public void SetTransition(Transition t) { machine.PerformTransition(t); }

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
        idle.AddTransition(Transition.PerformAttack, StateID.AttackState);

        AttackState attack = new AttackState();
        attack.AddTransition(Transition.NullTransition, StateID.IdleState);

        machine = new StateMachine();
        machine.AddState(idle);
        machine.AddState(attack);
    }
}
