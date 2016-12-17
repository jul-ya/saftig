using UnityEngine;
using InControl;

public abstract class State
{
    protected StateID stateID;
    public StateID ID { get { return stateID; } }
    
    public virtual void DoBeforeEntering() { }
    
    public virtual void DoBeforeLeaving() { }
    
    public abstract void Reason(GameObject player, InputDevice inputDevice);
    
    public abstract void Act(GameObject player, InputDevice inputDevice);

}
