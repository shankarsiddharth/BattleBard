public abstract class State
{
    protected Actor actor;
    protected StateMachine stateMachine;

    protected State(Actor actor, StateMachine stateMachine)
    {
        this.actor = actor;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() {
        //Debug.Log($"{actor} is in {GetType().ToString()}");
    }

    public virtual void Update() {}

    public virtual void Exit() {}
}