using System.Collections.Generic;
using UnityEngine;


public enum StateID
{
    NullState = 0,
    IdleState = 1,
    AttackState = 2,
    BlockState = 3,
    CrouchState = 4,
    TypingState = 5,
}


public class StateMachine
{
    private List<State> states;

    // The only way one can change the state of the FSM is by performing a transition
    // Don't change the CurrentState directly
    private StateID currentStateID;
    public StateID CurrentStateID { get { return currentStateID; } }
    private State currentState;
    public State CurrentState { get { return currentState; } }

    public StateMachine()
    {
        states = new List<State>();
    }
    
    public void AddState(State s)
    {
        // Check for Null reference before deleting
        if (s == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
        }

        // First State inserted is also the Initial state,
        //   the state the machine is in when the simulation begins
        if (states.Count == 0)
        {
            states.Add(s);
            currentState = s;
            currentStateID = s.ID;
            return;
        }

        // Add the state to the List if it's not inside it
        foreach (State state in states)
        {
            if (state.ID == s.ID)
            {
                Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() +
                               " because state has already been added");
                return;
            }
        }
        states.Add(s);
    }
    
    public void DeleteState(StateID id)
    {
        // Check for NullState before deleting
        if (id == StateID.NullState)
        {
            Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
            return;
        }

        // Search the List and delete the state if it's inside it
        foreach (State state in states)
        {
            if (state.ID == id)
            {
                states.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() +
                       ". It was not on the list of states");
    }
    
    public void PerformTransition(StateID id)
    {
        // Update the currentStateID and currentState		
        currentStateID = id;
        foreach (State state in states)
        {
            if (state.ID == currentStateID)
            {
                // Do the post processing of the state before setting the new one
                currentState.DoBeforeLeaving();

                currentState = state;

                // Reset the state to its desired condition before it can reason or act
                currentState.DoBeforeEntering();
                break;
            }
        }

    }

}
