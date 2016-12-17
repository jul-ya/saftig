using UnityEngine;
using InControl;

public abstract class State
{
    #region variables
    protected StateID stateID;

    protected float prepareTime;
    protected float performTime;
    protected float cooldownTime;

    protected float currentTime;
    #endregion

    #region properties
    public StateID ID { get { return stateID; } }
    #endregion

    #region methods
    protected State(float prepareTime, float performTime, float cooldownTime, StateID stateID)
    {
        this.prepareTime = prepareTime;
        this.performTime = performTime;
        this.cooldownTime = cooldownTime;
        this.stateID = stateID;
    }

    public virtual void DoBeforeEntering() { }
    
    public virtual void DoBeforeLeaving() { }
    
    public abstract void Reason(GameObject player, InputDevice inputDevice);
    
    public void Act(GameObject player, InputDevice inputDevice)
    {
        currentTime += Time.deltaTime;
        if (prepareTime > currentTime)
        {
            Prepare(player, inputDevice);
        }else if(prepareTime + performTime > currentTime)
        {
            PerformAction(player, inputDevice);
        }else
        {
            Cooldown(player, inputDevice);
        }
    }

    protected abstract void Prepare(GameObject player, InputDevice inputDevice);

    protected abstract void PerformAction(GameObject player, InputDevice inputDevice);

    protected abstract void Cooldown(GameObject player, InputDevice inputDevice);
    #endregion
}
