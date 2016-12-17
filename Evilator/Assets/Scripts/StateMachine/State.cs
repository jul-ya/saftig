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

	public Phase phase {
		get {
			if (prepareTime > currentTime)
			{
				return Phase.Prepare;
			}
			else if((prepareTime + performTime) > currentTime)
			{
				return Phase.Perform;
			}
			else if((prepareTime + performTime + cooldownTime) > currentTime)
			{
				return Phase.Cooldown;
			}
			else
			{
				return Phase.Concluded;
			}
		}
	}
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
    
    public void Reason(GameObject player, InputDevice inputDevice)
    {
        currentTime += Time.deltaTime;
        DoReason(player, inputDevice);
    }

    protected abstract void DoReason(GameObject player, InputDevice inputDevice);
    
    public void Act(GameObject player, InputDevice inputDevice)
    {
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
