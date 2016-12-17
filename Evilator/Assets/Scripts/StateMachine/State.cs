using UnityEngine;
using InControl;

public abstract class State : IStateVisitor
{
    #region variables
    protected StateID stateID;

    protected float prepareTime;
    protected float performTime;
    protected float cooldownTime;

    protected float currentTime;

	protected StateMachine machine;
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

	public void WasAddedTo(StateMachine machine) {
		this.machine = machine;
	}

    public virtual void DoBeforeEntering() { }
    
    public virtual void DoBeforeLeaving() {
        currentTime = 0;
    }
    
    public void Reason(GameObject player, InputDevice inputDevice)
    {
        currentTime += Time.deltaTime;
        DoReason(player, inputDevice);
    }

    protected abstract void DoReason(GameObject player, InputDevice inputDevice);
    
    public void Act(GameObject player, InputDevice inputDevice)
    {
		switch(phase)
		{
			case Phase.Prepare:
				Prepare(player, inputDevice);
				break;
			case Phase.Perform:
				PerformAction(player, inputDevice);
				break;
			case Phase.Cooldown:
				Cooldown(player, inputDevice);
				break;
			case Phase.Concluded:
				Conclude(player);
				break;
		}
    }

    protected abstract void Prepare(GameObject player, InputDevice inputDevice);

    protected abstract void PerformAction(GameObject player, InputDevice inputDevice);

    protected abstract void Cooldown(GameObject player, InputDevice inputDevice);

	protected abstract void Conclude(GameObject player);

	public abstract void Accept(IStateVisitor other);
	public virtual void Visit(AttackState attack) {}
	public virtual void Visit(BlockState block) {}
	public virtual void Visit(CrouchState crouch) {}
	public virtual void Visit(IdleState idle) {}
	public virtual void Visit(TypingState typing) {}
	public virtual void Visit(StunState stun) {}

    #endregion
}
