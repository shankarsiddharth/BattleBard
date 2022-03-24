public class IdleState : State
{
    public IdleState(Actor actor, StateMachine stateMachine) : base(actor, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        actor.target = null;
        actor.navMeshAgent.isStopped = true;
        actor.animator.SetTrigger("idle");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
